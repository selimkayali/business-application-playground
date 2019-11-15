namespace BookStore.BackOffice.WebApi.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public bool IsBestSeller { get; set; }
        public AuthorDto Author { get; set; }
    }
}