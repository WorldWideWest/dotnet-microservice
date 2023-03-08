using Api.Providers;
using Database;
using Database.Seed.IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Entities.Identity;

namespace Api.Extensions.Identity
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityServices(IServiceCollection services, string connectionString, string migrationAssembly)
        {
            var configurationMigrationAssembly = typeof(ConfigurationDbContext).Assembly.GetName().Name;

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<User>>("emailconfirmation");

            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });


            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = context => context.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(configurationMigrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = context => context.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(configurationMigrationAssembly));
                });
            
            return services;
        }

        public static void SeedIdentityConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in Configuration.Clients)
                {
                    var modifiedClient = new Client()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName,
                    };
                    context.Clients.Add(modifiedClient);

                }
                context.SaveChanges();
            }

            //if (!context.IdentityResources.Any())
            //{ 
            //    foreach (var resource in Configuration.IdentityResources)
            //        context.IdentityResources.Add(resource.ToEntity());
            //    context.SaveChanges();
            //}

            //if (!context.ApiScopes.Any())
            //{
            //    foreach (var resource in Configuration.ApiScopes)
            //        context.ApiScopes.Add(resource.ToEntity());
            //    context.SaveChanges();
            //}
        }
    }
}
