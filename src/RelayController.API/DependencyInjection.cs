using RelayController.Application;

namespace RelayController.API;

public static class DependencyInjection
{
   public static IServiceCollection RegisterAPIDependencies(this IServiceCollection services)
   {
       services.RegisterApplicationDependencies();
       return services;
   }
}