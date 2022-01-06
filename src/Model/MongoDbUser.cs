namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbUser : MongoDbUser<ObjectId>
{
    public MongoDbUser() : base() { }

    public MongoDbUser(string userName) : base(userName) { }

    public MongoDbUser(string email, string userName) : base(email, userName) { }
}

public class MongoDbUser<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    public MongoDbUser()
    {
        UserRoles = new List<MongoDbUserRole<TKey>>();
        UserClaims = new List<MongoDbUserClaim>();
        UserLogins = new List<MongoDbUserLogin>();
        UserTokens = new List<MongoDbUserToken>();
    }

    public MongoDbUser(string userName) : this()
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
    }

    public MongoDbUser(string email, string userName) : this()
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
    }

    public List<MongoDbUserRole<TKey>> UserRoles { get; set; }

    public List<MongoDbUserClaim> UserClaims { get; set; }

    public List<MongoDbUserLogin> UserLogins { get; set; }

    public List<MongoDbUserToken> UserTokens { get; set; }
}
