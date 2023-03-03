using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversitySample.Students.Domain.Dto;
using UniversitySample.Students.Service.Services;

namespace UniversitySample.Students.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;
        private readonly ILogger<StudentController> _logger;

        public StudentController(StudentService service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(IEnumerable<StudentDetailsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<IEnumerable<StudentDetailsDto>> GetAll()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id:guid}", Name = "GetById")]
        [ProducesResponseType(typeof(StudentDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<StudentDetailsDto> GetById(Guid id)
        {
            var course = _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpGet("GetByName/{name}", Name = "GetByName")]
        [ProducesResponseType(typeof(StudentDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult<StudentDetailsDto> GetByName(string name)
        {
            var course = _service.GetByName(name);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPut(Name = "AddStudent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult AddCourse(StudentDetailsDto studentDto)
        {
            _service.Add(studentDto);
            return Ok();
        }

        [HttpPost(Name = "UpdateStudent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult UpdateCourse(StudentDetailsDto studentDto)
        {
            try
            {
                _service.Update(studentDto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogDebug(ex, "Element not found");
                return NotFound();
            }
        }

        [HttpDelete("{id:guid}", Name = "DeleteStudent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult DeleteCourse(Guid id)
        {
            try
            {
                _service.Delete(id);
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