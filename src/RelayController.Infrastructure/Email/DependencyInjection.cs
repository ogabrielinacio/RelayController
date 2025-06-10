using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RelayController.Domain.Common;

namespace RelayController.Infrastructure.Email;

public static class DependencyInjection
{
  internal static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
  {
      services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

      var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>()
                        ?? throw new ArgumentNullException("Email section is missing");
      
      services.AddScoped<IEmailService, EmailService>();

       return services; 
  }
    
}