using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSample.Common.Communication;
using UniSample.Library.Domain.Contract;
using UniSample.Library.Domain.Dto;
using UniSample.Library.Service.Services;

namespace UniSample.Library.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LendingController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly LibraryService _libraryService;

        public LendingController(LibraryService libraryService, ILogger<BooksController> logger)
        {
            _libraryService = libraryService;
            _logger = logger;
        }

        [HttpPost(Name = "LendBook")]
        [Authorize(Roles = "Administrator,LibraryAdmin,Student")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<LendBookResponse>> LendBook(LendBookRequest request)
        {
            var response = await _libraryService.LendBook(request);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete(Name = "ReturnBook")]
        [Authorize(Roles = "Administrator,LibraryAdmin,Student")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<LendBookResponse>> ReturnBook(Guid lendingId)
        {
            var response = await _libraryService.ReturnBook(lendingId, User);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}