using Microsoft.EntityFrameworkCore;
using UniSample.Courses.Service.Model;

namespace UniSample.Courses.Service.DataAccess
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>().HasData(new Course()
            {
                Id = Guid.Parse("856EE11C-7A17-4247-9864-46A3D24A9D23"),
                Name = "Verteilte Systeme",
                ProfName = "Jochen Kluger",
                StudentsCount = 0
            },
            new Course()
            {
                Id = Guid.Parse("100EE899-CD2F-4120-A7C0-D6DF9341908E"),
                Name = "Cloud Native",
                ProfName = "Jochen Kluger",
                StudentsCount = 0
            });
        }
    }
}
