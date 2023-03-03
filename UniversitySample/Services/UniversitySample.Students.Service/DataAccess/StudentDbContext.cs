using Microsoft.EntityFrameworkCore;
using UniversitySample.Students.Service.Model;

namespace UniversitySample.Students.Service.DataAccess
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(SeedData.AdminStudent);
        }
    }
}
