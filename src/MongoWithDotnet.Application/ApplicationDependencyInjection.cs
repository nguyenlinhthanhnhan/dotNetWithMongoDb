using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoWithDotnet.Application.Services.AuthenticationService;
using MongoWithDotnet.Application.Services.AuthenticationService.Impl;
using MongoWithDotnet.Application.Services.AuthorizationService;
using MongoWithDotnet.Application.Services.AuthorizationService.Impl;

namespace MongoWithDotnet.Application;

public static class ApplicationDependencyInjection
{
    /// <summary>
    /// Add application dependencies
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="hostEnvironment"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
        IHostEnvironment hostEnvironment)
    {
        serviceCollection.AddServices(hostEnvironment);
        serviceCollection.RegisterAutoMapper();
        return serviceCollection;
    }

    /// <summary>
    /// Add services
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="hostEnvironment"></param>
    private static void AddServices(this IServiceCollection serviceCollection, IHostEnvironment hostEnvironment)
    {
        serviceCollection.AddScoped<IUserManager, UserManager>();
        serviceCollection.AddScoped<IRoleManager, RoleManager>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IAuthorizationService, AuthorizationService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}