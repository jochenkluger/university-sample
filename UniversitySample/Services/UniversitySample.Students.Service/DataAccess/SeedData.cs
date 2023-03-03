using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversitySample.Students.Service.Model;

namespace UniversitySample.Students.Service.DataAccess
{
    public class SeedData
    {
        public static Student AdminStudent = new Student()
        {
            Id = Guid.Parse("A626C0C6-4589-40F2-B970-5D25EB501EAC"),
            Username = "admin",
            FirstName = "Admin",
            LastName = "Student",
            EmailAddress = "admin.student@invalid.mail"
        };
    }
}
