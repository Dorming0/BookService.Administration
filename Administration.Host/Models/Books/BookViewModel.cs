using BookService.Core.Books;

namespace Administration.Host.Models.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public BookStatus Status { get; set; }
        public BookViewModel() { }
        public BookViewModel(Book book)
        {
            if(book != null)
            {
                Id = book.Id;
                Name = book.Name;
                Price = book.Price;
                Status = book.Status;
            }
        }
    }
}
