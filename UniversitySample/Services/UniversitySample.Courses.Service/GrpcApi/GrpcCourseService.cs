using CoreWCF;
using Grpc.Core;
using UniversitySample.Courses.Domain.Dto;
using UniversitySample.Courses.Domain.GrpcApi;
using UniversitySample.Courses.Service.ApiServices;
using UniversitySample.Courses.Service.InternalService;

namespace UniversitySample.Courses.Service.GrpcApi
{
    public class GrpcCourseService: GrpcCourses.GrpcCoursesBase
    {

        private readonly CourseProvider _provider;
        private readonly ILogger<GrpcCourseService> _logger;

        public GrpcCourseService(CourseProvider provider, ILogger<GrpcCourseService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        public override Task<GetCoursesResponse> Get(GetCoursesRequest request, ServerCallContext context)
        {
            List<CourseDetails?> result = new List<CourseDetails?>();
            if (string.IsNullOrEmpty(request.Name))
            {
                result = _provider.Get();
            }
            else
            {
                result = new List<CourseDetails?> { _provider.GetByName(request.Name) };
            }

            var response = new GetCoursesResponse();
            response.Courses.AddRange(Map(result));

            return Task.FromResult(response);
        }

        public override Task<GetCourseByIdResponse> GetById(GetCourseByIdRequest request, ServerCallContext context)
        {
            var id = Guid.Parse(request.Id);
            var result = _provider.GetById(id);

            var response = new GetCourseByIdResponse();
            response.Course = Map(result);

            return Task.FromResult(response);
        }

        public override Task<Response> Add(Course request, ServerCallContext context)
        {
            var courseDetails = Map(request);
            _provider.Add(courseDetails);
            return Task.FromResult(new Response());
        }

        public override Task<Response> Update(UpdateCourseRequest request, ServerCallContext context)
        {
            try
            {
                var courseDetails = Map(request.Course);
                _provider.Update(courseDetails);
                return Task.FromResult(new Response());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                return Task.FromResult(new Response() {ErrorMessage = ex.Message});
            }
        }

        public override Task<Response> Delete(DeleteCourseRequest request, ServerCallContext context)
        {
            try
            {
                _provider.Delete(Guid.Parse(request.Id));
                return Task.FromResult(new Response());
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                return Task.FromResult(new Response() { ErrorMessage = ex.Message });
            }
        }
        

        private static Course Map(CourseDetails courseDetails)
        {
            return new Course()
            {
                Id = courseDetails.Id.ToString(),
                Name = courseDetails.Name,
                StartDate = courseDetails.StartDate.ToString("O"),
                EndDate = courseDetails.EndDate.ToString("O"),
                CreatedDate = courseDetails.CreatedDate.ToString("O"),
                Description = courseDetails.Description,
                Professor = courseDetails.Professor
            };
        }

        private static IEnumerable<Course> Map(IEnumerable<CourseDetails> courseDetailsList)
        {
            var courseList = new List<Course>();
            courseList.AddRange(courseDetailsList.Select(x => Map(x)));
            return courseList;
        }

        private static CourseDetails Map(Course course)
        {
            return new CourseDetails()
            {
                Id = Guid.Parse(course.Id),
                Name = course.Name,
                StartDate = DateTime.Parse(course.StartDate),
                EndDate = DateTime.Parse(course.EndDate),
                CreatedDate = DateTime.Parse(course.CreatedDate),
                Description = course.Description,
                Professor = course.Professor
            };
        }
    }
}
