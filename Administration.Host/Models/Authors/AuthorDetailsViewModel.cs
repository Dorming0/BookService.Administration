using Administration.Host.Models.Books;
using Administration.Host.Models.Genres;
using BookService.Core.Authors;
using BookService.Core.Books;
using BookService.Core.Genres;

namespace Administration.Host.Models.Authors
{
    public class AuthorDetailsViewModel : AuthorViewModel
    {
        public string? Description { get; set; }
        public string? GenreName { get; set; }
        public string? BookName { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; } = Enumerable.Empty<GenreViewModel>();
        public IEnumerable<BookViewModel> Books { get; set; } = Enumerable.Empty<BookViewModel>();



        public AuthorDetailsViewModel() { }
        public AuthorDetailsViewModel(Author author) : base(author)
        {
            Description = author.Description;
            //GenreName = author.Genre?.Name;
            //BookName = author.Book?.Name;
            Genres = author.Genres.Select(x => new GenreViewModel(x));
            Books = author.Books.Select(x => new BookViewModel(x));
        }
    }
}