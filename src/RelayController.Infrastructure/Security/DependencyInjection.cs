using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RelayController.Domain.Common;
using RelayController.Infrastructure.Security.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace RelayController.Infrastructure.Security;

public static class DependencyInjection
{
   internal static IServiceCollection AddSecurityAuthentication(this IServiceCollection services, IConfiguration configuration)
   {
       services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

       var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()
                         ?? throw new ArgumentNullException("JwtSettings section is missing");

       var key = Encoding.ASCII.GetBytes(jwtSettings.Secret!);

       services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = true,
                   ValidIssuer = jwtSettings.Issuer,
                   ValidateAudience = true,
                   ValidAudience = jwtSettings.Audience,
                   ClockSkew = TimeSpan.Zero
               };
           });

       services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

       return services;
   }
}