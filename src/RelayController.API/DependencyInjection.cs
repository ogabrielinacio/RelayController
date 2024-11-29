using RelayController.Application;

namespace RelayController.API;

public static class DependencyInjection
{
   public static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
   {
       services.RegisterApplicationDependencies();
       return services;
   }
}