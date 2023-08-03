using BookService.Core.Genres;

namespace Administration.Host.Models.Genres
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GenreStatus Status { get; set; }

        public GenreViewModel() { }
        public GenreViewModel(Genre genre)
        {
            if (genre != null)
            {
                Id = genre.Id;
                Name = genre.Name;
                Description = genre.Description;
                Status = genre.Status;

            }
              
        }
    }
}
