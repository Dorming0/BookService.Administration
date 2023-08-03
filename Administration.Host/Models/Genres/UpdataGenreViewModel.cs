using BookService.Core.Genres;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Genres
{
    public class UpdataGenreViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage ="Поле обязательно к заполнению")]
        public string Name { get; set; }
        public string Description { get; set; }
        public UpdataGenreViewModel() { }
        public UpdataGenreViewModel(Genre genre)
        {
            Id= genre.Id;
            Name= genre.Name;
            Description= genre.Description;

        }
    }
}
