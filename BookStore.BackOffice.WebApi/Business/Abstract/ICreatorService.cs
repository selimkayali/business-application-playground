using System.Collections.Generic;
using BookStore.BackOffice.WebApi.Dto;

namespace BookStore.BackOffice.WebApi.Business.Abstract
{
    public interface ICreatorService
    {
        void CreateWord(IEnumerable<BookDto> bookList);
        void CreatePdf(IEnumerable<BookDto> bookList);
    }
}