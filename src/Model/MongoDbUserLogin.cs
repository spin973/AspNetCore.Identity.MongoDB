namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbUserLogin
{
    public string? LoginProvider { get; set; }

    public string? ProviderKey { get; set; }

    public string? ProviderDisplayName { get; set; }

    public MongoDbUserLogin()
    {
    }

    public MongoDbUserLogin(string loginProvider, string providerKey, string providerDisplayName)
    {
        LoginProvider = loginProvider;
        ProviderKey = providerKey;
        ProviderDisplayName = providerDisplayName;
    }
}
