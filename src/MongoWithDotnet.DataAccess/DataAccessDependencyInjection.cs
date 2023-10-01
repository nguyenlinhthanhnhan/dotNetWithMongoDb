using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoWithDotnet.DataAccess.Authorization;
using MongoWithDotnet.DataAccess.Persistence;
using MongoWithDotnet.DataAccess.Persistence.Configuration;
using MongoWithDotnet.DataAccess.Repositories;
using MongoWithDotnet.DataAccess.Repositories.Impl;
using MongoWithDotnet.Shared;

namespace MongoWithDotnet.DataAccess;

public static class DataAccessDependencyInjection
{
    /// <summary>
    /// Add data access dependencies
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDatabase(configuration);

        serviceCollection.AddRepositories();

        return serviceCollection;
    }

    /// <summary>
    /// Configure database
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        serviceCollection.AddSingleton<IMongoDbSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        var databaseConnection = GetEnvironmentVariable(AppCustomEnvironmentVariable.MongoServer,
            configuration.GetValue<string>("MongoDbSettings:MongoDbConnection") ??
            throw new InvalidOperationException("Connection string not found"));

        serviceCollection.AddSingleton<IMongoClient>(new MongoClient(databaseConnection));

        serviceCollection.AddSingleton<MongoDbContext>();

        serviceCollection.AddServices();
    }

    private static string GetEnvironmentVariable(string name, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? defaultValue;
    }

    private static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExampleRepository, ExampleRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
        serviceCollection.AddScoped<IPermissionRepository, PermissionRepository>();
    }

    private static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ApplicationAuthorizationProvider>();
        serviceCollection.AddSingleton<IPermissionManager, PermissionManager>();
    }
}