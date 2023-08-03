using BookService.Core.Authors;
using BookService.Core.Genres;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Books
{
    public class RegisterBookViewModel
    {
        [Required(ErrorMessage = "Наименование обязательно к заполнению")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Укажите цену книги")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Укажите автора книги")]
        public int AuthorId { get; set; }

        public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>();

        public RegisterBookViewModel() { }
        public RegisterBookViewModel(IEnumerable<Author> authors)
        {
            Authors = (from author in authors
                       select new SelectListItem
                       {
                           Value = author.Id.ToString(),
                           Text = author.Name,
                           Selected = AuthorId == author.Id
                       }).ToList();
        }
    }
}
