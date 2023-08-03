using BookService.Core.Books;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Administration.Host.Models.Books
{
    public class UpdataBookViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage ="Наименование обязательно к заполнению")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range (1, double.MaxValue,  ErrorMessage = "Укажите цену")]
        public decimal Price { get; set; }
        public UpdataBookViewModel() { }
        public UpdataBookViewModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Description = book.Descriptions;
            Price = book.Price;
        }

    }
}
