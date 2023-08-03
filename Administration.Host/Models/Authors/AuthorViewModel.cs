using BookService.Core.Authors;

namespace Administration.Host.Models.Authors
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public AuthorStatus Status { get; set; }
        public AuthorViewModel() { }
        public AuthorViewModel(Author author)
        {
            if (author != null)
            {
                Id = author.Id;
                Name = author.Name;
                Surname = author.Surname;
                Status = author.Status;
            }


        }
    }
}