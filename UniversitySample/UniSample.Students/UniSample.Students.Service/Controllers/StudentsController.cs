using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSample.Students.Domain.Dto;
using UniSample.Students.Service.DataAccess;
using UniSample.Students.Service.Services;

namespace UniSample.Students.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet(Name = "GetStudents")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(List<StudentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<List<StudentDto>>> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return Ok(students);
        }

        [HttpPost("init", Name = "InitStudent")]
        [Authorize()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<StudentDto>> InitStudent(StudentDto studentDto)
        {
            var returnDto = await _studentService.InitStudent(studentDto, studentDto.Identity);
            return Ok(returnDto);
        }

    }
}