using TimeRecorderAPI.DB;

namespace TimeRecorderAPI.Configuration {
    public static class DBConfiguration {
        public const string DBType = Program.EnvPrefix + "DB_TYPE";

        public static void AddDatabase(this IServiceCollection service) {
            string? dbType = Environment.GetEnvironmentVariable(DBType);
            switch (dbType?.ToLower()) {
                case "mongodb":
                    MongoManager mongoManager = new();
                    mongoManager.Init();
                    service.AddSingleton((IDataBaseManager) mongoManager);
                    break;
                default: throw new Exception($"Database type \"{dbType}\" not supported");
            }
        }
    }
}