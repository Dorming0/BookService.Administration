using Administration.Host.Models.Genres;
using BookService.Core.Authors;
using BookService.Core.Books;
using BookService.Core.Genres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceExtender.Data;

namespace Administration.Host.Models.Books
{
    public class BindGenreToBookViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
       

        public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
        public BindGenreToBookViewModel( int bookId, IEnumerable<Genre> genres)
        {
            Id = bookId;

            Genres = (from genre in genres
                      select new SelectListItem
                      {
                          Value = genre.Id.ToString(),
                          Text = genre.Name
                         
                      }).ToList();
        }
    }
}