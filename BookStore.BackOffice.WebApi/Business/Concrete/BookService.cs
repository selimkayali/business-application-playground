using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.DataAccess.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using BookStore.BackOffice.WebApi.Models;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class BookService:IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        
        
        
        public BookService(IMapper mapper,IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }
        public IEnumerable<BookDto> Get(int? beforeThisYear,int? afterThisYear,int? authorId,bool? isBestSeller )
        {

//            var bookList = _bookRepository.Get(book =>
//                (authorId == null || book.Author.Id == authorId)&&
//                                                      (beforeThisYear == null || book.PublicationYear<=beforeThisYear)&&
//                                                      (afterThisYear == null || book.PublicationYear>=afterThisYear)&&
//                (book.IsBestSeller==isBestSeller));
            var bookList = _bookRepository.Get(s=>s.IsBestSeller || !s.IsBestSeller);
          return _mapper.Map<IEnumerable<Book>,List<BookDto>>(bookList);
        }
    }
}