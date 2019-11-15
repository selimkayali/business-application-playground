using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookStore.BackOffice.WebApi.Models;

namespace BookStore.BackOffice.WebApi.DataAccess.Abstract
{
    public interface IBookRepository
    {
        IQueryable<Book> Get(Expression<Func<Book, bool>> predicate);
    }
}