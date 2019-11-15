using System.Collections.Generic;

namespace BookStore.BackOffice.WebApi.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<BookDto> Books { get; set; }
    }
}