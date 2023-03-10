using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSample.Common.Communication;
using UniSample.Library.Domain.Dto;
using UniSample.Library.Service.Services;

namespace UniSample.Library.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryUsersController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly LibraryService _libraryService;

        public LibraryUsersController(LibraryService libraryService, ILogger<BooksController> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet(Name = "GetLibraryUsers")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType(typeof(IEnumerable<LibraryUserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<LibraryUserDto>>> GetLibraryUsers()
        {
            var libraryUsers = await _libraryService.GetLibraryUsers();
            return Ok(libraryUsers);
        }

        [HttpGet("/api/LibraryUsers/GetById/{id:guid}", Name = "GetLibraryUserById")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType(typeof(LibraryUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<LibraryUserDto>> GetLibraryUserById(Guid id)
        {
            var libraryUser = await _libraryService.GetLibraryUserById(id);
            return Ok(libraryUser);
        }

        [HttpGet("/api/LibraryUsers/GetByStudentId/{id:guid}", Name = "GetLibraryUserByStudentId")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType(typeof(LibraryUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<LibraryUserDto>> GetLibraryUserByStudentId(Guid id)
        {
            var libraryUser = await _libraryService.GetLibraryUserByStudentId(id);
            return Ok(libraryUser);
        }

        [HttpPut(Name = "AddLibraryUser")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddLibraryUser(LibraryUserDto libraryUserDto)
        {
            await _libraryService.AddLibraryUser(libraryUserDto);
            return Ok();
        }
    }
}