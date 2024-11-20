using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RelayController.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(mSvcConfig => mSvcConfig.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}