using Microsoft.AspNetCore.Mvc;

namespace Administration.Host.Models.Authors
{
    public class RegisterAuthorViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
    }
}