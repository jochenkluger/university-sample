using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using UniSample.Common.Communication;
using UniSample.Common.Exceptions;
using UniSample.Library.Domain.Contract;
using UniSample.Library.Domain.Dto;
using UniSample.Library.Domain.Validations;
using UniSample.Library.Service.DataAccess;
using UniSample.Library.Service.Model;

namespace UniSample.Library.Service.Services
{
    public class LibraryService
    {
        private readonly ILogger<LibraryService> _logger;
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<BookDto> _bookValidator;

        public LibraryService(LibraryDbContext dbContext, IMapper mapper, IValidator<BookDto> bookValidator, ILogger<LibraryService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _bookValidator = bookValidator;
            _logger = logger;
        }

        public async Task<List<BookDto>> GetBooks(string filter)
        {
            //TODO: use filter
            var bookModels = await _dbContext.Books
                .Include(x => x.Lendings)
                .ThenInclude(x => x.LibraryUser).ToListAsync();
            var bookDtos = _mapper.Map<List<BookDto>>(bookModels);
            return bookDtos;
        }

        public async Task<BookDto> GetBookById(Guid id)
        {
            var bookModel = await _dbContext.Books.Include(x => x.Lendings).FirstOrDefaultAsync(x => x.Id == id);
            var bookDto = _mapper.Map<BookDto>(bookModel);
            return bookDto;
        }

        public async Task DeleteBook(Guid id)
        {
            var course = await _dbContext.Books.Include(x => x.Lendings).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                throw new EntityNotFoundException(id.ToString());
            }

            _dbContext.Books.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddBook(BookDto bookDto)
        {
            await _bookValidator.ValidateAndThrowAsync(bookDto);
            var bookModel = _mapper.Map<Book>(bookDto);

            var existing =
                await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == bookModel.Id );
            if (existing != null)
            {
                throw new EntityAlreadyExistsException();
            }

            await _dbContext.Books.AddAsync(bookModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpsertBook(BookDto bookDto)
        {
            await _bookValidator.ValidateAndThrowAsync(bookDto);
            var bookModel = _mapper.Map<Book>(bookDto);

            var existing =
                await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == bookModel.Id);
            if (existing != null)
            {
                _dbContext.Books.Update(bookModel);
            }
            else
            {
                await _dbContext.Books.AddAsync(bookModel);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task<LendBookResponse> LendBook(LendBookRequest request)
        {
            var bookFromDb =
                await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == request.BookId);
            if (bookFromDb == null)
            {
                throw new EntityNotFoundException(request.BookId.ToString());
            }

            var libraryUser = await _dbContext.LibraryUsers.FirstOrDefaultAsync(x => x.Id == request.LibraryUserId);
            if (libraryUser == null)
            {
                throw new EntityNotFoundException(request.LibraryUserId.ToString());
            }

            var response = new LendBookResponse();

            if (bookFromDb.Available == false)
            {
                response.Success = false;
                response.Error = "Das gewünschte Buch ist aktuell nicht verfügbar.";
                return response;
            }

            bookFromDb.Available = false;
            _dbContext.Books.Update(bookFromDb);

            var lending = new Lending()
            {
                Id = Guid.NewGuid(),
                Book = bookFromDb,
                LibraryUser = libraryUser,
                StartTime = DateTime.Now
            };

            await _dbContext.Lendings.AddAsync(lending);
            await _dbContext.SaveChangesAsync();

            response.LendingDto = _mapper.Map<LendingDto>(lending);
            response.Success = true;
            return response;
        }

        public async Task<Response> ReturnBook(Guid lendingId, ClaimsPrincipal claimsPrincipal)
        {
            var lendingFromDb = await _dbContext.Lendings.FirstOrDefaultAsync(x => x.Id == lendingId);
            if (lendingFromDb == null)
            {
                throw new EntityNotFoundException(lendingId.ToString());
            }

            var response = new Response();
            if (lendingFromDb.ReturnTime != null)
            {
                response.Success = false;
                response.Error = "Das ausgeliehene Buch ist bereits zurück gegeben!";
                return response;
            }

            var userId = Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "sub")!.Value);

            if (lendingFromDb.LibraryUser.Id != userId)
            {
                response.Success = false;
                response.Error = "Das ausgeliehene Buch kann nur von dem Benutzer zurückgegeben werden, der es geliehen hat!";
                return response;
            }

            lendingFromDb.Book.Available = true;
            _dbContext.Books.Update(lendingFromDb.Book);

            lendingFromDb.ReturnTime = DateTime.Now;
            _dbContext.Lendings.Update(lendingFromDb);
            await _dbContext.SaveChangesAsync();
            response.Success = true;

            return response;
        }

        public async Task<List<LibraryUserDto>> GetLibraryUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<LibraryUserDto?> GetLibraryUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<LibraryUserDto?> GetLibraryUserByStudentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddLibraryUser(LibraryUserDto libraryUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
