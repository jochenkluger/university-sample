using Microsoft.EntityFrameworkCore;

namespace UniSample.Library.Service.DataAccess
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddLibraryDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            var provider = services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("Db");

            Action<DbContextOptionsBuilder>? optionsAction = options =>
            {
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(3);
                    builder.MigrationsAssembly(typeof(LibraryDbContext).Assembly.FullName);
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            };
            services.AddDbContextFactory<LibraryDbContext>(optionsAction);
            services.AddDbContext<LibraryDbContext>(optionsAction, lifeTime);

            return services;
        }
    }
}
