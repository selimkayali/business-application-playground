namespace BookStore.BackOffice.WebApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public bool IsBestSeller { get; set; }
        public virtual Author Author { get; set; }
    }
}