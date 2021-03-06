# AspNetCore.Identity.MongoDB
This is the .NET 6.0 version of the repository [AspNetCore.Identity.Mongo](https://github.com/matteofabbri/AspNetCore.Identity.Mongo), it allows you to use MongoDb instead of SQL server with Microsoft.AspNetCore.Identity 3.1.

## Dot Net Core Versions support

Library supports [`ASP.NET Core 6.0`](https://docs.microsoft.com/it-it/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0).

## Using the Library

[The library is available at NuGet.org](https://www.nuget.org/packages/AspNetCore.IdentityProvider.MongoDB).

### Sample

You can find a sample application under [./samples](./samples) folder. This is a simple asp.net core 6.0 with scaffolding identity that use MongoDB as repository.

### Tests

In order to be able to run the tests, you need to have MongoDB up and running on `localhost:27017`. You can easily do this by running the below Docker command:

```bash
docker run --name some-mongo -d -p "27017:27017" mongo:3
```

After that, you can run the tests through the `dotnet test` command under the test project directory.

## How to use:
AspNetCore.Identity.MongoDB is installed from NuGet:
```
Install-Package AspNetCore.IdentityProvider.MongoDB
```
The simplest way to set up:
```csharp
using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Model;

// in Program.cs 
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddIdentity<MongoDbUser>()
    .AddMongoDbStores(); //use defaults: MongoDbUser, MongoDbRole with primary key of type MongoDB.Bson.ObjectId
```

You can personalize the connections parameter with an `Action<MongoDbOptions>` parameter:
```csharp
using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Model;

// in Program.cs 
builder.Services
    .AddIdentity<MongoDbUser>()
    .AddMongoDbStores(options =>
    {
        options.ConnectionString = "mongodb://localhost:27017";  // default "mongodb://localhost/default"
        options.DatabaseName = "MongoIdentityDb";                // default "identityDB"
        options.UsersCollection = "myUsersCollection";           // default "identity.users"
        options.RolesCollection = "myRolesCollection";           // default "identity.roles"
    });
```
Or You can also configure an instance of MongoClientSettings:
```csharp
using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Model;
using MongoDB.Driver;

// in Program.cs 
builder.Services
    .AddIdentity<MongoDbUser, MongoDbRole>()
    .AddMongoDbStores(options =>
    {
        options.MongoClientSettings = new MongoClientSettings()
        {
            ApplicationName = "mongoApp",
            Credential = MongoCredential.CreateCredential("source", "username", "password"),
            Servers = new[] { new MongoServerAddress("localhost"), new MongoServerAddress("127.0.0.1", 27017) },
            // other options
        };
    });
```

Using User and Role models:
```csharp
using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Model;

// in Program.cs 
builder.Services
    .AddIdentity<MongoDbUser, MongoDbRole>()
    .AddMongoDbStores();
```

Using different type of the primary key (default is `MongoDB.Bson.ObjectId`):
```csharp
using AspNetCore.Identity.MongoDB;
using AspNetCore.Identity.MongoDB.Model;

public class ApplicationUser : MongoDbUser<string>
{
}

public class ApplicationRole : MongoDbRole<string>
{
}

// in Program.cs 
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores();
```

## How to Contribute
Before create any issue/PR please look at the [CONTRIBUTING](./CONTRIBUTING.md)

## Code of conduct
See [CODE_OF_CONDUCT](./CODE_OF_CONDUCT.md)

## License
This project is licensed under the [MIT license](./LICENSE.txt)

## Support This Project
If you have found this project helpful, either as a library that you use or as a learning tool, please consider offering me a coffee: <a href="https://www.buymeacoffee.com/spin973" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" style="max-height: 40px;width: 150px !important;" ></a>

