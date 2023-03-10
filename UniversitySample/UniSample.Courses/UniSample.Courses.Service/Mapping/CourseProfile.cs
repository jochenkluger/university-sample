using AutoMapper;
using UniSample.Courses.Domain.Dto;
using UniSample.Courses.Service.Model;

namespace UniSample.Courses.Service.Mapping
{
    public class CourseProfile: Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();

            CreateMap<CourseStudentDto, CourseStudent>();
            CreateMap<CourseStudent, CourseStudentDto>();
        }
    }
}
