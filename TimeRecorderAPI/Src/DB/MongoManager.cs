using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleNetLogger;
using TimeRecorderDomain;

namespace TimeRecorderAPI.DB {
    public sealed class MongoManager : IDataBaseManager {
        private const string UserEnv = Program.EnvPrefix + "MONGODB_USER";
        private const string PassEnv = Program.EnvPrefix + "MONGODB_PASS";
        private const string IPEnv = Program.EnvPrefix + "MONGODB_IP";
        private const string PortEnv = Program.EnvPrefix + "MONGODB_PORT";
        private const string DBNameEnv = Program.EnvPrefix + "MONGODB_DBNAME";

        public event EventHandler<DataBaseConnectedEventArgs>? DBConnected;
        public event EventHandler<DataBaseDisconnectedEventArgs>? DBDisconnected;

        public bool IsConnected;
        private IMongoDatabase? _db;

        private bool _firstTime = true;

        public void Init() {
            string? user = Environment.GetEnvironmentVariable(UserEnv);
            string? pass = Environment.GetEnvironmentVariable(PassEnv);
            string? ip = Environment.GetEnvironmentVariable(IPEnv);
            string? port = Environment.GetEnvironmentVariable(PortEnv);
            string? dbName = Environment.GetEnvironmentVariable(DBNameEnv)?.ToLower();

            CheckEnvironmentVariables(user, pass, ip, port,
                                      dbName);

            try {
                MongoClientSettings settings = new() {
                    Server = new MongoServerAddress(ip, int.Parse(port!)),
                    Credential = MongoCredential.CreateCredential("admin", user, pass),
                };

                _db = new MongoClient(settings).GetDatabase(dbName);

                try {
                    IsConnected = _db.RunCommandAsync((Command<BsonDocument>) "{ping:1}").Wait(2000);
                } catch (Exception) {
                    IsConnected = false;
                }

                switch (IsConnected) {
                    case true:
                        OnDataBaseConnected();
                        break;
                    case false when _firstTime:
                        OnDataBaseDisconnected();
                        break;
                }
                _firstTime = false;
            } catch (Exception e) {
                Logger.Error(e);
            }
        }

        private static void CheckEnvironmentVariables(string? mongoUser, string? mongoPass, string? mongoIP, string? mongoPort,
                                                      string? mongoDBName) {
            bool missUser = string.IsNullOrWhiteSpace(mongoUser);
            bool missPass = string.IsNullOrWhiteSpace(mongoPass);
            bool missIP = string.IsNullOrWhiteSpace(mongoIP);
            bool missPort = string.IsNullOrWhiteSpace(mongoPort);
            bool missDBName = string.IsNullOrWhiteSpace(mongoDBName);

            if (!missUser && !missPass && !missIP && !missPort && !missDBName) return;

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Missing environment variables:");
            if (missUser) stringBuilder.AppendLine($"- {UserEnv}");
            if (missPass) stringBuilder.AppendLine($"- {PassEnv}");
            if (missIP) stringBuilder.AppendLine($"- {IPEnv}");
            if (missPort) stringBuilder.AppendLine($"- {PortEnv}");
            if (missDBName) stringBuilder.AppendLine($"- {DBNameEnv}");
            Logger.Error(stringBuilder.ToString());
            Environment.Exit(1);
        }

        private bool CheckStatus() {
            if (_db == null) return false;
            if (!_db.RunCommandAsync((Command<BsonDocument>) "{ping:1}").Wait(1000)) {
                IsConnected = false;
                Logger.Error("Database disconnected");
                OnDataBaseDisconnected();
                return false;
            }
            if (!IsConnected) {
                Logger.Info("Database connected");
                OnDataBaseConnected();
            }
            IsConnected = true;
            return true;
        }

        public async Task<T?> Find<T>(string id) where T : IDBObject, new() {
            List<T> list = await Find<T>(id, 1);
            return list.Count == 0 ? default : list[0];
        }

        public async Task<T?> Find<T>(Expression<Func<T, bool>> filter) where T : IDBObject, new() {
            List<T> list = await Find(filter, 1);
            return list.Count == 0 ? default : list[0];
        }

        public async Task<List<T>> Find<T>(string id, int quantity) where T : IDBObject, new() {
            if (string.IsNullOrWhiteSpace(id)) return []; 
            return await Find<T>(v => v.ID == id, quantity);
        }

        public async Task<List<T>> Find<T>(Expression<Func<T, bool>> filter, int quantity) where T : IDBObject, new() {
            try {
                if (quantity < 0) quantity = 0;
                List<T> list = await _db!.GetCollection<T>(typeof(T).Name.ToLower()).Find(filter).Limit(quantity).ToListAsync();
                return list;
            } catch (MongoConnectionException) {
                CheckStatus();
                return [];
            }
        }

        public async Task<List<T>> FindAll<T>() where T : IDBObject, new() {
            return await Find<T>(v => true, 0);
        }

        public async Task<T?> FindLast<T>() where T : IDBObject, new() {
            try {
                SortDefinition<T> sort = Builders<T>.Sort.Descending("time");
                return await _db!.GetCollection<T>(typeof(T).Name.ToLower()).Find(v => true).Sort(sort).FirstOrDefaultAsync();
            } catch (MongoConnectionException) {
                CheckStatus();
                return default;
            }
        }

        public async Task Insert<T>(T obj) where T : IDBObject, new() {
            try {
                T? item = await Find<T>(obj.ID);
                if (item == null) await _db!.GetCollection<T>(typeof(T).Name.ToLower()).InsertOneAsync(obj);
                else await Replace(obj);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task InsertAll<T>(IEnumerable<T> list) where T : IDBObject, new() {
            try {
                // Make a dictionary with all objects in the database to speed up the search
                Logger.Info($"Building dictionary ({typeof(T).Name})");
                List<T> objects = await FindAll<T>();
                Dictionary<string, T> mongoDbObjectsDict =
                    objects.ToDictionary(x => x.ID, StringComparer.InvariantCultureIgnoreCase);

                List<T> existObjectsList = [];
                List<T> notExistObjectsList = [];

                // Split objects in two lists, one with objects that already exist in the database and another with new objects
                Logger.Info($"Checking new items ({typeof(T).Name})");
                foreach (T replaceObj in list) {
                    if (mongoDbObjectsDict.ContainsKey(replaceObj.ID)) {
                        existObjectsList.Add(replaceObj);
                    } else {
                        notExistObjectsList.Add(replaceObj);
                    }
                }

                // Insert new objects
                Logger.Info($"Inserting many new items ({typeof(T).Name})");
                if (notExistObjectsList.Count >= 1)
                    await _db!.GetCollection<T>(typeof(T).Name.ToLower()).InsertManyAsync(notExistObjectsList);
                Logger.Info($"Finish insert new items ({typeof(T).Name})");

                // Replace old objects
                Logger.Info($"Replacing old items ({typeof(T).Name})");
                if (existObjectsList.Count >= 1) await ReplaceAll(existObjectsList);
                Logger.Info($"Finish replacing old items ({typeof(T).Name})");
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task Replace<T>(T obj) where T : IDBObject, new() {
            try {
                await _db!.GetCollection<T>(typeof(T).Name.ToLower()).ReplaceOneAsync(v => v.ID == obj.ID, obj);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task ReplaceAll<T>(List<T> list) where T : IDBObject, new() {
            if (list.Count == 0) return;
            try {
                IMongoCollection<T> collection = _db!.GetCollection<T>(typeof(T).Name.ToLower());
                await Parallel.ForEachAsync(list,
                                            async (x, ct) => {
                                                await collection.ReplaceOneAsync(v => v.ID == x.ID, x, cancellationToken: ct);
                                            });
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task Delete<T>(string id) where T : IDBObject, new() {
            try {
                await _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteOneAsync(x => x.ID == id);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task Delete<T>(T obj) where T : IDBObject, new() {
            try {
                await Delete<T>(obj.ID);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task Delete<T>(Predicate<T> predicate) where T : IDBObject, new() {
            try {
                List<T> list = await FindAll<T>();
                List<string> idsToDelete = list.FindAll(predicate).Select(x => x.ID).ToList();
                if (idsToDelete.Count > 0) {
                    await _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteManyAsync(x => idsToDelete.Contains(x.ID));
                }
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public async Task Delete<T>(List<T> toDeleteProducts) where T : IDBObject, new() {
            if (toDeleteProducts.Count == 0) return;
            try {
                List<string> idsToDelete = toDeleteProducts.Select(x => x.ID).ToList();
                await _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteManyAsync(x => idsToDelete.Contains(x.ID));
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        private void OnDataBaseConnected() {
            EventHandler<DataBaseConnectedEventArgs>? handler = DBConnected;
            handler?.Invoke(this, new DataBaseConnectedEventArgs());
        }

        private void OnDataBaseDisconnected() {
            EventHandler<DataBaseDisconnectedEventArgs>? handler = DBDisconnected;
            handler?.Invoke(this, new DataBaseDisconnectedEventArgs());
        }
    }

    public class DataBaseConnectedEventArgs : EventArgs;

    public class DataBaseDisconnectedEventArgs : EventArgs;
}