namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbRoleClaim
{
    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public MongoDbRoleClaim()
    {
    }

    public MongoDbRoleClaim(string claimType, string claimValue)
    {
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}
