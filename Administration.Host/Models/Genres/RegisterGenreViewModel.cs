using BookService.Core.Authors;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Genres
{
    public class RegisterGenreViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
       
}
