namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbUserRole : MongoDbUserRole<ObjectId>
{
    public MongoDbUserRole(ObjectId id, string name) : base(id, name)
    {
    }
}

public class MongoDbUserRole<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }

    public string Name { get; set; }

    public MongoDbUserRole(TKey id, string name)
    {
        Id = id;
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is MongoDbUserRole<TKey> userRole)
        {
            return userRole.Id.Equals(Id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
