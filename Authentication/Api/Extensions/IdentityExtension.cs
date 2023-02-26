using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Identity;

namespace Api.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityServices(IServiceCollection services, string connectionString, string migrationAssembly) 
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = context => context.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = context => context.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationAssembly));
                });

            return services;
        }
    }
}
