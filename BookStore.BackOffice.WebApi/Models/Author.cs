using System.Collections.Generic;

namespace BookStore.BackOffice.WebApi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}