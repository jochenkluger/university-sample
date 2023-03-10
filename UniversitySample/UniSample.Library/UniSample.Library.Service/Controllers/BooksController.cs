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
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly LibraryService _libraryService;

        public BooksController(LibraryService libraryService, ILogger<BooksController> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpGet(Name = "GetBooks")]
        [Authorize(Roles = "Administrator,LibraryAdmin,Student")]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery] string filter)
        {
            var books = await _libraryService.GetBooks(filter);
            return Ok(books);
        }

        [HttpGet("/api/Books/GetById/{id:guid}", Name = "GetBookById")]
        [Authorize(Roles = "Administrator,LibraryAdmin,Student")]
        [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            var book = await _libraryService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost(Name = "UpdateBook")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateBook(BookDto book)
        {
            await _libraryService.UpsertBook(book);
            return Ok();
        }

        [HttpPut(Name = "AddBook")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AddBook(BookDto book)
        {
            await _libraryService.AddBook(book);
            return Ok();
        }

        [HttpDelete("{id:guid}", Name = "DeleteBook")]
        [Authorize(Roles = "Administrator,LibraryAdmin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            await _libraryService.DeleteBook(id);
            return Ok();
        }
    }
}