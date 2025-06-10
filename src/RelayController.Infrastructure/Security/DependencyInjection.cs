using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RelayController.Domain.Common;
using RelayController.Infrastructure.Security.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RelayController.Domain.Enums;


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
       services.AddAuthorization(options =>
       {
           options.AddPolicy(AuthorizationPolicies.RequireConfirmEmail, policy =>
               policy.RequireClaim("purpose", TokenPurpose.ConfirmEmail.ToString()));

           options.AddPolicy(AuthorizationPolicies.RequireResetPassword, policy =>
               policy.RequireClaim("purpose", TokenPurpose.ResetPassword.ToString()));

           options.AddPolicy(AuthorizationPolicies.RequireAuthentication, policy =>
               policy.RequireClaim("purpose", TokenPurpose.Authentication.ToString()));
       });

       services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

       return services;
   }
}