using System.ComponentModel.DataAnnotations;

namespace UniSample.Courses.Service.Model
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProfName { get; set; }
        public int StudentsCount { get; set; }
        public List<CourseStudent> Students { get; set; } = new List<CourseStudent>();
    }
}
