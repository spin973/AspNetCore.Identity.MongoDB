namespace AspNetCore.Identity.MongoDB.Test;

public class MongoDbUserTest
{
    [Fact]
    public async Task MongoDbUser_CanBeCreatedAndRetrivedAndDeleted()
    {
        var username = "Admininistrator";
        var countryName = "IT";
        var loginProvider = "Local";
        var providerKey = Guid.NewGuid().ToString();
        var providerDisplayName = "Local Account";
        var email = "administrator@local.it";
        var user = new MongoDbUser(email, username);
        user.UserClaims.Add(new MongoDbUserClaim(ClaimTypes.Country, countryName));
        user.UserLogins.Add(new MongoDbUserLogin(loginProvider, providerKey, providerDisplayName));

        using var dbProvider = MongoDbServerTestUtils.CreateDatabase();

        var userStore = new UserStore<MongoDbUser, MongoDbRole, ObjectId>(dbProvider.UserCollection, dbProvider.RoleCollection, null);

        var result = await userStore.CreateAsync(user, CancellationToken.None);
        Assert.True(result.Succeeded);

        var retrievedUser = await userStore.FindByIdAsync(user.Id.ToString(), CancellationToken.None);
        Assert.NotNull(retrievedUser);
        Assert.Equal(username, retrievedUser.UserName);
        Assert.Equal(email, retrievedUser.Email);

        retrievedUser = await userStore.FindByNameAsync(user.UserName.ToUpperInvariant(), CancellationToken.None);
        Assert.NotNull(retrievedUser);
        Assert.Equal(username, retrievedUser.UserName);
        Assert.Equal(email, retrievedUser.Email);

        var retrivedClaim = retrievedUser.UserClaims.FirstOrDefault(x => x.ClaimType == ClaimTypes.Country);
        Assert.NotNull(retrivedClaim);
        Assert.Equal(countryName, retrivedClaim?.ClaimValue);

        var retrievedLoginProvider = retrievedUser.UserLogins.FirstOrDefault(x => x.LoginProvider == loginProvider);
        Assert.NotNull(retrievedLoginProvider);
        Assert.Equal(providerKey, retrievedLoginProvider?.ProviderKey);
        Assert.Equal(providerDisplayName, retrievedLoginProvider?.ProviderDisplayName);

        var deleteUser = await userStore.DeleteAsync(user, CancellationToken.None);
        Assert.True(deleteUser.Succeeded);
    }

    [Fact]
    public async Task MongoDbUser_AddtoRoleAndIsInRole()
    {
        var username = "Admininistrator";
        var email = "administrator@local.it";
        var user = new MongoDbUser(email, username);

        var roleName = "Admin";
        var role = new MongoDbRole(roleName);

        using var dbProvider = MongoDbServerTestUtils.CreateDatabase();

        var roleStore = new RoleStore<MongoDbRole, ObjectId>(dbProvider.RoleCollection, null);
        var userStore = new UserStore<MongoDbUser, MongoDbRole, ObjectId>(dbProvider.UserCollection, dbProvider.RoleCollection, null);

        var userManager = new UserManager<MongoDbUser>(userStore, null, null, null, null, null, null, null, null);

        var userResult = await userManager.CreateAsync(user);
        Assert.True(userResult.Succeeded);

        var roleResult = await roleStore.CreateAsync(role, CancellationToken.None);
        Assert.True(roleResult.Succeeded);

        await userManager.AddToRoleAsync(user, roleName);

        var isInRole = await userManager.IsInRoleAsync(user, roleName);
        Assert.True(isInRole);

        var usersInRole = await userManager.GetUsersInRoleAsync(roleName);
        Assert.Contains(usersInRole, x => x.UserName == user.UserName);
    }

}
