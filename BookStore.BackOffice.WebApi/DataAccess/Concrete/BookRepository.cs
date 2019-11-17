using System;
using System.Linq;
using System.Linq.Expressions;
using BookStore.BackOffice.WebApi.DataAccess.Abstract;
using BookStore.BackOffice.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BackOffice.WebApi.DataAccess.Concrete
{
    
    public class BookRepository:IBookRepository
    {
        private readonly DbSet<Book> _dbSet;
        public BookRepository(BookStoreDbContext dbContext)
        {
            this._dbSet = dbContext.Set<Book>();
        }
        public IQueryable<Book> Get(Expression<Func<Book, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}