using GraphQL;
using Microsoft.AspNetCore.Cors.Infrastructure;
using UniversitySample.Courses.Domain.Dto;
using UniversitySample.Courses.Service.Interfaces;

namespace UniversitySample.Courses.Service.GraphQl.Schema
{
    public class Query
    {
        public static CourseDetails? FirstCourse([FromServices] CoursesGraphQlService courseService)
            => courseService.Get().FirstOrDefault();

        public static IEnumerable<CourseDetails> AllCourses([FromServices] CoursesGraphQlService courseService, string? name = null)
            => name == null ? courseService.Get() : new List<CourseDetails>() { courseService.GetByName(name)};

        public static int Count([FromServices] CoursesGraphQlService courseService)
            => courseService.Get().Count;
    }
}
