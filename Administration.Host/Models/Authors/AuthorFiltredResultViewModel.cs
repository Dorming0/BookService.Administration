using Administration.Host.Models.Books;

namespace Administration.Host.Models.Authors
{
    public class AuthorFiltredResultViewModel
    {
        public int TotalCount { get; set; }
        public int ItemsPageCount { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<AuthorViewModel> Items { get; set; } = Enumerable.Empty<AuthorViewModel>();

        public AuthorFiltredResultViewModel() { }
        public AuthorFiltredResultViewModel(int totalCount, int itemsPageCount, IEnumerable<AuthorViewModel> items)
        {
            TotalCount = totalCount;
            ItemsPageCount = itemsPageCount;
            PageCount = (int)Math.Ceiling((double)TotalCount / ItemsPageCount);
            Items = items;
        }
    }
}
