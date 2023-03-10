using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniSample.Common.Exceptions;
using UniSample.Courses.Domain.Dto;
using UniSample.Courses.Service.Services;

namespace UniSample.Courses.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService, ILogger<CoursesController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet(Name = "GetCourses")]
        [SwaggerOperation(OperationId = "GetCourses", Tags = new[] { "Courses" }, Summary = "Get a list of courses", Description = "This endpoint can be invoked to get a list of all courses")]
        [Authorize(Roles = "Administrator,Student")]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetCourses();
            return Ok(courses);
        }

        [HttpGet("/api/Courses/GetById/{id:guid}", Name = "GetCourseById")]
        [Authorize(Roles = "Administrator,Student")]
        [ProducesResponseType(typeof(CourseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CourseDto>> GetCourseById(Guid id)
        {
            try
            {
                var course = await _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "UpdateCourse")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateCourse(CourseDto course)
        {
            await _courseService.UpsertCourse(course);
            return Ok();
        }

        [HttpPut(Name = "AddCourse")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddCourse(CourseDto course)
        {
            try
            {
                await _courseService.AddCourse(course);
                return Ok();
            }
            catch (EntityAlreadyExistsException ex)
            {
                _logger.LogError(ex, "Fehler beim Einfügen - {e}", ex.Message);
                return BadRequest("Element existiert bereits");
            }
        }

        [HttpPost("/api/Courses/Enroll",Name = "EnrollCourse")]
        [Authorize(Roles = "Student")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> EnrollCourse(CourseStudentDto courseStudentDto)
        {
            await _courseService.EnrollCourse(courseStudentDto);
            return Ok();
        }
    }
}