using Administration.Host.Models.Genres;
using BookService.Core.Books.Abstractions;
using BookService.Core.Genres.Abstractions;
using BookService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceExtender.Http;

namespace Administration.Host.Controllers
{
    [Authorize(Roles = "admin")]
    public class GenreController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
      

        protected ISrvUser SrvUser => new AdminSrvUSer(HttpContext.User);

        public GenreController(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
           
        }

        [HttpGet]
        public async Task<IActionResult> Filtred()
        {
            var genres = await _genreService.FiltredAsync(0, int.MaxValue, null, SrvUser);
            if (genres == null || genres?.Items == null)
            {
                return View(new GenreFiltredResultViewModel());
            }
            var result = new GenreFiltredResultViewModel(genres.TotalCount, genres.PageCount, genres.Items.Select(x => new GenreViewModel(x)));
            return View(result);
        }

        public async Task<IActionResult> Find(int genreId)
        {
            var genres = await _genreService.FindAsync(x => x.Id == genreId, SrvUser);
            var result = new GenresDetailsViewModel(genres);
            return View(result);
        }
        public IActionResult Registration()
        {
            return View(new RegisterGenreViewModel());
        }


        [HttpPost]
        public IActionResult Registration(RegisterGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _genreService.Registration(new BookService.Core.Genres.Genre(model.Name, model.Description));
            return RedirectToAction("Filtred");
        }
        public async Task<IActionResult> Updata(int genreId)
        {
            var puc = await _genreService.FindAsync(x => x.Id == genreId, SrvUser);
             var result = new UpdataGenreViewModel(puc);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Updata(UpdataGenreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var genre = await _genreService.FindAsync(x => x.Id == model.Id,SrvUser);
            _genreService.Update(new BookService.Core.Genres.Genre(model.Name, model.Description, model.Id));
            return RedirectToAction("Filtred");
        }
      
        public IActionResult Delete(int genreId)
        {
            _genreService.Delete(genreId);
            return RedirectToAction("Filtred");
        }
        public async Task<IActionResult> ChangeStatus(int genreId)
        {
            await _genreService.ChangeStatus(genreId);
            return RedirectToAction("Filtred", new { genreId });
        }
    }
}
