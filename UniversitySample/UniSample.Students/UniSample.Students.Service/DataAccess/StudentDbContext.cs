using Microsoft.EntityFrameworkCore;
using UniSample.Students.Service.Model;

namespace UniSample.Students.Service.DataAccess
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

    }
}
