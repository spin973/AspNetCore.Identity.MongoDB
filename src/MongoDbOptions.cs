namespace AspNetCore.Identity.MongoDB;

public class MongoDbOptions
{
    public string ConnectionString { get; set; } = "mongodb://localhost/default";
    public string DatabaseName { get; set; } = "identityDB";
    public string UsersCollection { get; set; } = "identity.users";
    public string RolesCollection { get; set; } = "identity.roles";
    public MongoClientSettings? MongoClientSettings { get; set; }
}
