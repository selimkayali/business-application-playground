using System.Collections.Generic;
using BookStore.BackOffice.WebApi.Business.Abstract;
using BookStore.BackOffice.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.BackOffice.WebApi.Business.Concrete
{
    public class PdfReportCreator:ICreatorService
    {
        public FileStreamResult CreateReport(IEnumerable<BookDto> bookList)
        {
            throw new System.NotImplementedException();
        }
    }
}