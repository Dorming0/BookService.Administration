using BookService.Core.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Authors
{
    public class UpdataAuthorViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string Surname { get; set; }
        public string Description { get; set; }
        public UpdataAuthorViewModel() { }
        public UpdataAuthorViewModel(Author author)
        {
            Id = author.Id;
            Name = author.Name;
            Surname = author.Surname;
            Description = author.Description;
        }
    }
}