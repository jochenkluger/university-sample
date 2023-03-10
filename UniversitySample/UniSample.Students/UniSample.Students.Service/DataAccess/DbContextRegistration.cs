using Microsoft.EntityFrameworkCore;

namespace UniSample.Students.Service.DataAccess
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddStudentDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            var provider = services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("Db");

            Action<DbContextOptionsBuilder>? optionsAction = options =>
            {
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(3);
                    builder.MigrationsAssembly(typeof(StudentDbContext).Assembly.FullName);
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            };
            services.AddDbContextFactory<StudentDbContext>(optionsAction);
            services.AddDbContext<StudentDbContext>(optionsAction, lifeTime);

            return services;
        }
    }
}
