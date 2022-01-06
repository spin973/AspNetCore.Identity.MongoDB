var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string connectionString = "mongodb://localhost:27017";
const string databaseName = "MongoIdentityDb";

builder.Services
    .AddIdentity<MongoDbUser, MongoDbRole>()
    .AddMongoDbStores(options =>
    {
        options.ConnectionString = connectionString;
        options.DatabaseName = databaseName;
    })
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
