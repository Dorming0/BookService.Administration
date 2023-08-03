using Administration.Host.Models.Authors;
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
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;

        protected ISrvUser SrvUser => new AdminSrvUSer(HttpContext.User);

        public AuthorController(IAuthorService authorService, IBookService bookService, IGenreService genreService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        [HttpGet]
        public async Task<IActionResult> Filtred()
        {
            var author = await _authorService.FiltredAsync(0, int.MaxValue, null, 
                (author, user) => null, SrvUser);
            if(author == null || author?.Items == null)
            {
                return View(new AuthorFiltredResultViewModel());
            }

            var result = new AuthorFiltredResultViewModel(author.TotalCount, author.PageCount,
                author.Items.Select(x => new AuthorViewModel(x)));

            return View(result);
        }

      
        public async Task<IActionResult> Find(int authorId)
        {
            var author = await _authorService.FindAsync(x => x.Id == authorId, SrvUser);
            var result = new AuthorDetailsViewModel(author);
                return View(result);
        }

        public IActionResult Registration()
        {
            var result = new RegisterAuthorViewModel();

            return View(result);
        }

        [HttpPost]
        public IActionResult Registration(RegisterAuthorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _authorService.Registration(new BookService.Core.Authors.Author(model.Name, model.Surname, model.Description));

            return Redirect("Filtred");
        }

        public async Task<IActionResult> Update(int authorId)
        {
            var author = await _authorService.FindAsync(x => x.Id == authorId, SrvUser, HttpContext.RequestAborted);

            var result = new UpdataAuthorViewModel(author);

            return View(result);
        }

        [HttpPost]
        public IActionResult Update(UpdataAuthorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _authorService.Update(new BookService.Core.Authors.Author(model.Name, model.Surname,model.Description, model.Id));
            return Redirect("Filtred");
        }
        //[HttpPost]
        public IActionResult Delete(int authorId)
        {
            _authorService.Delite(authorId);
            return RedirectToAction("Filtred");
        }
        public async Task<IActionResult> ChangeStatus(int authorId)
        {
            await _authorService.ChangeStatus(authorId);
            return RedirectToAction("Filtred", new {authorId});
        }
    }
}
