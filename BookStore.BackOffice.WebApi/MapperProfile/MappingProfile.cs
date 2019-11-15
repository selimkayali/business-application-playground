using AutoMapper;
using BookStore.BackOffice.WebApi.Dto;
using BookStore.BackOffice.WebApi.Models;

namespace BookStore.BackOffice.WebApi.MapperProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            
            
        }
        
    }
}