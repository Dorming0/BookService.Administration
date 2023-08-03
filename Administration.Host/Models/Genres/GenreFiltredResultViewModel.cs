namespace Administration.Host.Models.Genres
{
    public class GenreFiltredResultViewModel
    {
        public int TotalCount { get; set; }
        public int ItemsPageCount { get; set; }
        public int PageCount { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public GenreFiltredResultViewModel() { }
        public GenreFiltredResultViewModel(int totalCount, int itemsPageCount, IEnumerable<GenreViewModel> genres)
        {
            TotalCount = totalCount;
            ItemsPageCount = itemsPageCount;
            PageCount = (int)Math.Ceiling((double)TotalCount / ItemsPageCount);
            Genres = genres;
        }
    }
}
