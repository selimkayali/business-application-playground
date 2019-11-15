using System.Collections.Generic;
using BookStore.BackOffice.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.BackOffice.WebApi.Business.Abstract
{
    public interface ICreatorService
    {
        FileStreamResult CreateWord(IEnumerable<BookDto> bookList);
        FileStreamResult CreatePdf(IEnumerable<BookDto> bookList);
    }
}