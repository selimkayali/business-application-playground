using System.Collections;
using System.Collections.Generic;
using BookStore.BackOffice.WebApi.Dto;

namespace BookStore.BackOffice.WebApi.Business.Abstract
{
    public interface IBookService
    {
        IEnumerable<BookDto> Get(int? beforeThisYear,int? afterThisYear,int? authorId,bool? isBestSeller);
    }
}