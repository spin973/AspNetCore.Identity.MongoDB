namespace AspNetCore.Identity.MongoDB;

public static class MongoDbServicesExtensions
{
    public static IdentityBuilder AddMongoDbStores(this IdentityBuilder builder,
        Action<MongoDbOptions> setupDatabaseAction)
    {
        return AddMongoDbStores<MongoDbUser, MongoDbRole, ObjectId>(builder, setupDatabaseAction);
    }

    public static IdentityBuilder AddMongoDbStores<TUser>(this IdentityBuilder builder)
            where TUser : MongoDbUser
    {
        return AddMongoDbStores<TUser, MongoDbRole<ObjectId>, ObjectId>(builder, x => { });
    }

    public static IdentityBuilder AddMongoDbStores<TUser>(this IdentityBuilder builder,
        Action<MongoDbOptions> setupDatabaseAction)
        where TUser : MongoDbUser
    {
        return AddMongoDbStores<TUser, MongoDbRole, ObjectId>(builder, setupDatabaseAction);
    }

    public static IdentityBuilder AddMongoDbStores<TUser, TRole>(this IdentityBuilder builder)
        where TUser : MongoDbUser
        where TRole : MongoDbRole
    {
        return AddMongoDbStores<TUser, TRole, ObjectId>(builder, x => { });
    }

    public static IdentityBuilder AddMongoDbStores<TUser, TRole>(this IdentityBuilder builder,
        Action<MongoDbOptions> setupDatabaseAction)
        where TUser : MongoDbUser
        where TRole : MongoDbRole
    {
        return AddMongoDbStores<TUser, TRole, ObjectId>(builder, setupDatabaseAction);
    }

    public static IdentityBuilder AddMongoDbStores<TUser, TRole, TKey>(this IdentityBuilder builder, Action<MongoDbOptions> setupDatabaseAction)
        where TKey : IEquatable<TKey>
        where TUser : MongoDbUser<TKey>
        where TRole : MongoDbRole<TKey>
    {
        var dbOptions = new MongoDbOptions();
        setupDatabaseAction(dbOptions);

        var client = dbOptions.MongoClientSettings is null ?
            new MongoClient(dbOptions.ConnectionString) :
            new MongoClient(dbOptions.MongoClientSettings);

        var database = client.GetDatabase(dbOptions.DatabaseName);

        var userCollection = database.GetCollection<TUser>(dbOptions.UsersCollection);
        var roleCollection = database.GetCollection<TRole>(dbOptions.RolesCollection);

        builder.AddRoleStore<RoleStore<TRole, TKey>>();
        builder.AddUserStore<UserStore<TUser, TRole, TKey>>();
        builder.AddUserManager<UserManager<TUser>>();
        builder.AddRoleManager<RoleManager<TRole>>();

        builder.Services.TryAddSingleton(x => userCollection);
        builder.Services.TryAddSingleton(x => roleCollection);

        if (typeof(TKey) == typeof(ObjectId))
        {
            TypeConverterResolver.RegisterTypeConverter<ObjectId, ObjectIdConverter>();
        }

        return builder;
    }

}
