using Administration.Host.Models.Books;
using BookService.Core.Authors.Abstractions;
using BookService.Core.Books.Abstractions;
using BookService.Core.Genres.Abstractions;
using BookService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceExtender.Http;

namespace Administration.Host.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        protected ISrvUser SrvUser
           => new AdminSrvUSer(HttpContext.User);


        public BookController(IBookService bookService, IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        [HttpGet]
        public async Task<IActionResult> Filtred(/*FilterParamsViewModel filter*/)
        {
            var books = await _bookService.FiltredAsync(0, int.MaxValue, null, SrvUser);
            if (books == null || books?.Items == null)
            {
                return View(new BookFiltredResultViewModel());
            }

            var result = new BookFiltredResultViewModel(books.TotalCount, books.PageCount,
                books.Items.Select(x => new BookViewModel(x)));

            return View(result);
        }

        public async Task<IActionResult> Find(int bookId)
        {
            var book = await _bookService.FindAsync(x => x.Id == bookId, SrvUser);

            var result = new BookDetailsViewModel(book);

            return View(result);
        }

        public async Task<IActionResult> Registration()
        {
            var authors = await _authorService.FiltredAsync(0, int.MaxValue, null, SrvUser);

            var result = new RegisterBookViewModel(authors.Items);
            return View(result);
        }

        [HttpPost]
        public IActionResult Registration(RegisterBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _bookService.Registration
                (new BookService.Core.Books.Book(model.Name, model.Description, model.Price, model.AuthorId));

            return RedirectToAction("Filtred");
        }

        public async Task<IActionResult> Update(int bookId)
        {
            var puc = await _bookService.FindAsync(x => x.Id == bookId, SrvUser, HttpContext.RequestAborted);
            var result = new UpdataBookViewModel(puc);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdataBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var book = await _bookService.FindAsync(x => x.Id == model.Id, SrvUser);

            _bookService.Update
                (new BookService.Core.Books.Book(model.Name, model.Description, model.Price, book.Author.Id, book.Id));

            return RedirectToAction("Filtred");
        }
    
        public IActionResult Delete(int bookId)
        {
            _bookService.Delete(bookId);
            return RedirectToAction("Filtred");
        }
        public async Task<IActionResult> BindGenre(int bookId)
        {
            var book = await _bookService.FindAsync(x=>x.Id==bookId,SrvUser);
            if(book == null)
            {
                return NotFound();
            }
            var genre = await _genreService.FiltredAsync(0, int.MaxValue, null, SrvUser, HttpContext.RequestAborted);
            var result = new BindGenreToBookViewModel(book.Id, genre.Items);
            return View(result);
        }
        public async Task<IActionResult> BindedGenre (int bookId, int genreId)
        {
          await _bookService.BindGenreAsync(bookId, genreId);
            return RedirectToAction("Find", new { bookId });

        }
        public async Task<IActionResult> UntieGenre(int bookId, int genreId)
        {
            await _bookService.UntieGenreAsync(bookId, genreId);
            return RedirectToAction("Find", new { bookId});

        }
        public async Task<IActionResult> ChangeStatus (int bookId)
        {
             await _bookService.ChangeStatus(bookId);
            return RedirectToAction("Filtred", new { bookId });
        }
    }
}
