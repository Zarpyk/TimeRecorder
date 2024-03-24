using System.Linq.Expressions;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleNetLogger;
using TimeRecorderDomain;

namespace TimeRecorderServer.DB {
    public class MongoManager : IDataBaseManager {
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

                    //TODO QUICK_FIX connection leak on CheckStatus (InsertAll)
                    //TODO System.AggregateException: One or more errors occurred.
                    //TODO (The wait queue for acquiring a connection to server localhost:27017 is full.
                    //https://stackoverflow.com/questions/37322110/mongowaitqueuefullexception-the-wait-queue-for-acquiring-a-connection-to-server
                    MaxConnectionPoolSize = 1000
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
                OnDataBaseDisconnected();
                return false;
            }
            if (!IsConnected) OnDataBaseConnected();
            IsConnected = true;
            return true;
        }

        public T? Find<T>(string id, bool checkStatus = true) where T : IDBObject, new() {
            List<T> list = Find<T>(id, 1, checkStatus);
            return list.Count == 0 ? default : list[0];
        }

        public T? Find<T>(Expression<Func<T, bool>> filter, bool checkStatus = true) where T : IDBObject, new() {
            List<T> list = Find(filter, 1, checkStatus);
            return list.Count == 0 ? default : list[0];
        }

        public List<T> Find<T>(string id, int quantity, bool checkStatus = true) where T : IDBObject, new() {
            return Find<T>(v => v.ID == id, quantity, checkStatus);
        }

        public List<T> Find<T>(Expression<Func<T, bool>> filter, int quantity, bool checkStatus = true)
            where T : IDBObject, new() {
            if (checkStatus && !CheckStatus()) return new List<T>();
            try {
                if (quantity < 0) quantity = 0;
                List<T> list = _db!.GetCollection<T>(typeof(T).Name.ToLower()).Find(filter).Limit(quantity).ToList();
                return list;
            } catch (MongoConnectionException) {
                CheckStatus();
                return new List<T>();
            }
        }

        public List<T> FindAll<T>(bool checkStatus = true) where T : IDBObject, new() {
            if (checkStatus && !CheckStatus()) return new List<T>();
            return Find<T>(v => true, 0, false);
        }

        public T? FindLast<T>() where T : IDBObject, new() {
            if (!CheckStatus()) return default;
            SortDefinition<T> sort = Builders<T>.Sort.Descending("time");
            return _db!.GetCollection<T>(typeof(T).Name.ToLower()).Find(v => true).Sort(sort).FirstOrDefault();
        }

        public void Insert<T>(T obj) where T : IDBObject, new() {
            if (!CheckStatus()) return;
            try {
                T? item = Find<T>(obj.ID);
                if (item == null) _db!.GetCollection<T>(typeof(T).Name.ToLower()).InsertOne(obj);
                else Replace(obj);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void InsertAll<T>(IEnumerable<T> list) where T : IDBObject, new() {
            if (!CheckStatus()) return;
            try {
                Logger.Info($"Building dictionary ({typeof(T).Name})");
                Dictionary<string, T> mongoDbObjectsDict = FindAll<T>(false)
                   .ToDictionary(x => x.ID, StringComparer.InvariantCultureIgnoreCase);

                List<T> existObjectsList = new();
                List<T> notExistObjectsList = new();

                Logger.Info($"Checking new items ({typeof(T).Name})");
                foreach (T replaceObj in list) {
                    if (mongoDbObjectsDict.ContainsKey(replaceObj.ID)) {
                        existObjectsList.Add(replaceObj);
                    } else {
                        notExistObjectsList.Add(replaceObj);
                    }
                }
                Logger.Info($"Inserting many new items ({typeof(T).Name})");
                if (notExistObjectsList.Count >= 1)
                    _db!.GetCollection<T>(typeof(T).Name.ToLower()).InsertMany(notExistObjectsList);
                Logger.Info($"Finish insert new items ({typeof(T).Name})");
                Logger.Info($"Replacing old items ({typeof(T).Name})");
                if (existObjectsList.Count >= 1) ReplaceAll(existObjectsList, false);
                Logger.Info($"Finish replacing old items ({typeof(T).Name})");
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void Replace<T>(T obj, bool checkStatus = true) where T : IDBObject, new() {
            if (checkStatus && !CheckStatus()) return;
            try {
                _db!.GetCollection<T>(typeof(T).Name.ToLower()).ReplaceOne(v => v.ID == obj.ID, obj);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void ReplaceAll<T>(List<T> list, bool checkStatus = true) where T : IDBObject, new() {
            if (checkStatus && !CheckStatus()) return;
            if (list.Count == 0) return;
            try {
                IMongoCollection<T> collection = _db!.GetCollection<T>(typeof(T).Name.ToLower());
                Parallel.ForEach(list, x => { collection.ReplaceOne(v => v.ID == x.ID, x); });
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void Delete<T>(string id) where T : IDBObject, new() {
            if (!CheckStatus()) return;
            try {
                _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteOne(x => x.ID == id);
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void Delete<T>(T obj) where T : IDBObject, new() {
            Delete<T>(obj.ID);
        }

        public void Delete<T>(Predicate<T> predicate) where T : IDBObject, new() {
            if (!CheckStatus()) return;
            try {
                List<T> list = FindAll<T>(false);
                List<string> idsToDelete = list.FindAll(predicate).Select(x => x.ID).ToList();
                if (idsToDelete.Count > 0) {
                    _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteMany(x => idsToDelete.Contains(x.ID));
                }
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        public void Delete<T>(List<T> toDeleteProducts) where T : IDBObject, new() {
            if (!CheckStatus()) return;
            if (toDeleteProducts.Count == 0) return;
            try {
                List<string> idsToDelete = toDeleteProducts.Select(x => x.ID).ToList();
                _db!.GetCollection<T>(typeof(T).Name.ToLower()).DeleteMany(x => idsToDelete.Contains(x.ID));
            } catch (MongoConnectionException) {
                CheckStatus();
            }
        }

        protected virtual void OnDataBaseConnected() {
            EventHandler<DataBaseConnectedEventArgs>? handler = DBConnected;
            handler?.Invoke(this, new DataBaseConnectedEventArgs());
        }

        protected virtual void OnDataBaseDisconnected() {
            EventHandler<DataBaseDisconnectedEventArgs>? handler = DBDisconnected;
            handler?.Invoke(this, new DataBaseDisconnectedEventArgs());
        }
    }

    public class DataBaseConnectedEventArgs : EventArgs;

    public class DataBaseDisconnectedEventArgs : EventArgs;
}