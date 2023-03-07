using Api.Extensions.Identity;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Identity;
using Models.Interfaces.Services;
using Services;
using System.Reflection;

namespace Api.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            string migrationAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddLogging();

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.DisableImplicitFromServicesParameters = true;
            //});

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            ApiVersioningExtension.AddApiVersioningServices(services);
            RateLimitterExtension.AddRateLimiterServices(services);
            IdentityExtension.AddIdentityServices(services, connectionString, migrationAssembly);
            AutoMapperExtension.AddAutoMapperServices(services);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUrlHelper>(options =>
            {
                var actionContext = options.GetService<IActionContextAccessor>()
                    .ActionContext;

                var factory = options.GetRequiredService<IUrlHelperFactory>();

                return factory.GetUrlHelper(actionContext);
            });

            //services.AddMailKit(config =>
            //{
            //    var options = builder.Configuration.GetSection("MailSettings").Get<MailKitOptions>();
            //    config.UseMailKit(options);
            //});

            return services;
        }
    }
}
