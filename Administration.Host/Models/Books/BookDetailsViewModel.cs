using Administration.Host.Models.Genres;
using BookService.Core.Authors;
using BookService.Core.Books;

namespace Administration.Host.Models.Books
{
    public class BookDetailsViewModel : BookViewModel
    {
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; } = Enumerable.Empty<GenreViewModel>();

        public BookDetailsViewModel() { }
        public BookDetailsViewModel(Book book) : base(book)
        {
            Description = book.Descriptions;
            AuthorId = book.Author.Id;
            AuthorName = book.Author.Name;
            Genres = book.Genres.Select(x => new GenreViewModel(x));
        }
    }
}
