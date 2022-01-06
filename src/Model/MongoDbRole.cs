namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbRole : MongoDbRole<ObjectId>
{
    public MongoDbRole() : base() { }

    public MongoDbRole(string name) : base(name) { }
}

public class MongoDbRole<TKey> : IdentityRole<TKey> where TKey : IEquatable<TKey>
{
    public MongoDbRole()
    {
        RoleClaims = new List<MongoDbRoleClaim>();
    }

    public MongoDbRole(string name) : this()
    {
        Name = name;
        NormalizedName = name.ToUpperInvariant();
    }

    public List<MongoDbRoleClaim> RoleClaims { get; set; }
}
