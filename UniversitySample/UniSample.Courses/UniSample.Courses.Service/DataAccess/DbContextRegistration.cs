using Microsoft.EntityFrameworkCore;

namespace UniSample.Courses.Service.DataAccess
{
    public static class DbContextRegistration
    {
        public static IServiceCollection AddCourseDb(this IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            var provider = services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("Db");

            Action<DbContextOptionsBuilder>? optionsAction = options =>
            {
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(3);
                    builder.MigrationsAssembly(typeof(CourseDbContext).Assembly.FullName);
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
            };
            services.AddDbContextFactory<CourseDbContext>(optionsAction);
            services.AddDbContext<CourseDbContext>(optionsAction, lifeTime);

            return services;
        }
    }
}
