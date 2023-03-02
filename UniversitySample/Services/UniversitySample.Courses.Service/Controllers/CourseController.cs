using System.Net;
using Microsoft.AspNetCore.Mvc;
using UniversitySample.Courses.Domain.Dto;
using UniversitySample.Courses.Service.InternalService;

namespace UniversitySample.Courses.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseProvider _provider;
        private readonly ILogger<CourseController> _logger;

        public CourseController(CourseProvider provider, ILogger<CourseController> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<CourseDetails>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<IEnumerable<CourseDetails>> GetAll()
        {
            return Ok(_provider.Get());
        }

        [HttpGet("{id:guid}",Name = "GetById")]
        [ProducesResponseType(typeof(CourseDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<CourseDetails> GetById(Guid id)
        {
            var course = _provider.GetById(id);
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
            var course = _provider.GetByName(name);
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
            _provider.Add(course);
            return Ok();
        }

        [HttpPost(Name = "UpdateCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult UpdateCourse(CourseDetails courseDto)
        {
            try
            {
                _provider.Update(courseDto); 
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex,"Element not found");
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}", Name = "DeleteCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult DeleteCourse(Guid id)
        {
            try
            {
                _provider.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                return NotFound();
            }
        }
    }
}