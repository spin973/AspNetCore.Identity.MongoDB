namespace AspNetCore.Identity.MongoDB.Test
{
    public class MongoDbRoleTest
    {
        [Fact]
        public async Task MongoDbRole_CanBeCreatedAndRetrivedAndDeleted()
        {
            var roleName = "User";
            var role = new MongoDbRole(roleName);

            using var dbProvider = MongoDbServerTestUtils.CreateDatabase();

            var roleStore = new RoleStore<MongoDbRole, ObjectId>(dbProvider.RoleCollection, null);

            var roleResult = await roleStore.CreateAsync(role, CancellationToken.None);
            Assert.True(roleResult.Succeeded);

            var retrievedRole = await roleStore.FindByNameAsync(role.Name.ToUpperInvariant(), CancellationToken.None);
            Assert.NotNull(retrievedRole);
            Assert.Equal(roleName, retrievedRole.Name);

            retrievedRole = await roleStore.FindByIdAsync(role.Id.ToString(), CancellationToken.None);
            Assert.NotNull(retrievedRole);
            Assert.Equal(roleName, retrievedRole.Name);

            var deleteRole = await roleStore.DeleteAsync(role, CancellationToken.None);
            Assert.True(deleteRole.Succeeded);
        }
    }
}