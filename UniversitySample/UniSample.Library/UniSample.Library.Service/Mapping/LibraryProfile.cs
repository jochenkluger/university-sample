using AutoMapper;
using UniSample.Library.Domain.Dto;
using UniSample.Library.Service.Model;

namespace UniSample.Library.Service.Mapping
{
    public class LibraryProfile: Profile
    {
        public LibraryProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();

            CreateMap<LibraryUserDto, LibraryUser>();
            CreateMap<LibraryUser, LibraryUserDto>();

            CreateMap<LendingDto, Lending>();
            CreateMap<Lending, LendingDto>();
        }
    }
}
