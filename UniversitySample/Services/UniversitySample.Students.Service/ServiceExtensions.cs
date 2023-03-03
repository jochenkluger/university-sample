using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversitySample.Students.Service.DataAccess;

namespace UniversitySample.Students.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddStudentDb(this IServiceCollection services,
            ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            var provider = services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("Default");

            Action<DbContextOptionsBuilder> optionAction = optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(connectionString, contextOptionsBuilder =>
                {
                    contextOptionsBuilder.MigrationsAssembly(typeof(StudentDbContext).Assembly.FullName);
                });
            };

            services.AddDbContextFactory<StudentDbContext>(optionAction);
            services.AddDbContext<StudentDbContext>(optionAction);
            
            return services;
        }
    }
}
