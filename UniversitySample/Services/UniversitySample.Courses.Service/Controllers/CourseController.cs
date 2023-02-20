using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniversitySample.Courses.Domain.Dto;

namespace UniversitySample.Courses.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private static List<CourseDetails> _courseList = new List<CourseDetails>()
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

        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<CourseDetails>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<IEnumerable<CourseDetails>> GetAll()
        {
            return Ok(_courseList);
        }

        [HttpGet("{id:guid}",Name = "GetById")]
        [ProducesResponseType(typeof(CourseDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<CourseDetails> GetById(Guid id)
        {
            var course = _courseList.FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpGet("GetByName/{name}", Name = "GetByName")]
        [ProducesResponseType(typeof(CourseDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<CourseDetails> GetByName(string name)
        {
            var course = _courseList.FirstOrDefault(x => x.Name == name);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPut(Name = "AddCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult AddCourse(CourseDetails course)
        {
            _courseList.Add(course);
            return Ok();
        }

        [HttpPost(Name = "UpdateCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult UpdateCourse(CourseDetails courseDto)
        {
            var course = _courseList.FirstOrDefault(x => x.Id == courseDto.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.StartDate = courseDto.StartDate;
            course.EndDate = courseDto.EndDate;

            return Ok();
        }

        [HttpDelete("{id:guid}", Name = "DeleteCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult DeleteCourse(Guid id)
        {
            var course = _courseList.FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            
            _courseList.Remove(course);

            return Ok();
        }
    }
}