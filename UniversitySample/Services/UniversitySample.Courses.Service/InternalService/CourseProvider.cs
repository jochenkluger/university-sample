using UniversitySample.Courses.Domain.Dto;

namespace UniversitySample.Courses.Service.InternalService
{
    public class CourseProvider
    {
        private static List<CourseDetails?> _courseList = new List<CourseDetails?>()
        {
            new CourseDetails
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(30),
                CreatedDate = DateTime.Now.AddDays(-100)
            },
            new CourseDetails
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                StartDate = DateTime.Now.AddDays(-5),
                EndDate = DateTime.Now.AddDays(50),
                CreatedDate = DateTime.Now.AddDays(-100)
            },
            new CourseDetails
            {
                Id = Guid.NewGuid(),
                Name = "Test3",
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(40),
                CreatedDate = DateTime.Now.AddDays(-100)
            }
        };

        public List<CourseDetails?> Get()
        {
            return _courseList;
        }

        public CourseDetails? GetById(Guid id)
        {
            return _courseList.FirstOrDefault(x => x?.Id == id);
        }

        public CourseDetails? GetByName(string name)
        {
            return _courseList.FirstOrDefault(x => x?.Name == name);
        }

        public void Add(CourseDetails courseDetails)
        {
            _courseList.Add(courseDetails);
        }

        public void Update(CourseDetails courseDetails)
        {
            if (courseDetails == null)
            {
                throw new KeyNotFoundException(courseDetails?.Id.ToString());
            }
            var courseToChange = _courseList.FirstOrDefault(x => x?.Id == courseDetails?.Id);
            if (courseToChange == null)
            {
                throw new KeyNotFoundException(courseDetails?.Id.ToString());
            }

            courseToChange.Name = courseDetails.Name;
            courseToChange.Description = courseDetails.Description;
            courseToChange.StartDate = courseDetails.StartDate;
            courseToChange.EndDate = courseDetails.EndDate;
        }

        public void Delete(Guid id)
        {
            var courseToDelete = _courseList.FirstOrDefault(x => x?.Id == id);
            if (courseToDelete == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }
            _courseList.Remove(courseToDelete);
        }
    }
}
