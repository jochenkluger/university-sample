namespace UniSample.Students.Service.Model
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Identity { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
