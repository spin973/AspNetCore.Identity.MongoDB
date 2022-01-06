namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbUserClaim
{
    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public MongoDbUserClaim()
    {
    }

    public MongoDbUserClaim(string claimType, string claimValue)
    {
        ClaimType = claimType;
        ClaimValue = claimValue;
    }
}
