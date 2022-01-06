namespace AspNetCore.Identity.MongoDB.Model;

public class MongoDbUserToken
{
    public string? LoginProvider { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public MongoDbUserToken()
    {
    }

    public MongoDbUserToken(string loginProvider, string name, string value)
    {
        LoginProvider = loginProvider;
        Name = name;
        Value = value;
    }    
}
