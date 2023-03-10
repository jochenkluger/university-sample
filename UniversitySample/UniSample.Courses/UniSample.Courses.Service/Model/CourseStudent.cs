using System.ComponentModel.DataAnnotations;

namespace UniSample.Courses.Service.Model
{
    public class CourseStudent
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
    }
}
