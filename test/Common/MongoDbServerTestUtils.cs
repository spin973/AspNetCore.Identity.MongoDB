namespace AspNetCore.Identity.MongoDB.Test.Common;

internal static class MongoDbServerTestUtils
{
    public static DisposableDatabase CreateDatabase() => new();

    public class DisposableDatabase : IDisposable
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<MongoDbUser> _userCollection;
        private readonly IMongoCollection<MongoDbRole> _roleCollection;

        public DisposableDatabase()
        {
            var databaseName = Guid.NewGuid().ToString("N");
            var userCollectionName = "users";
            var roleCollectionName = "roles";

            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _database = _mongoClient.GetDatabase(databaseName);
            _userCollection = _database.GetCollection<MongoDbUser>(userCollectionName);
            _roleCollection = _database.GetCollection<MongoDbRole>(roleCollectionName);

            TypeDescriptor.AddAttributes(typeof(ObjectId), new Attribute[1] { new TypeConverterAttribute(typeof(ObjectIdConverter)) });
        }

        public IMongoDatabase Database => _database;
        public IMongoCollection<MongoDbUser> UserCollection => _userCollection;
        public IMongoCollection<MongoDbRole> RoleCollection => _roleCollection;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mongoClient.DropDatabase(_database.DatabaseNamespace.DatabaseName);
            }
        }
    }
}
