using Administration.Host.Models.Authors;

namespace Administration.Host.Models.Books
{
    public class BookFiltredResultViewModel
    {
        public int TotalCount { get; set; }
        public int ItemsPageCount { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<BookViewModel> Items { get; set; } = Enumerable.Empty<BookViewModel>();

        public BookFiltredResultViewModel() { }
        public BookFiltredResultViewModel(int totalCount, int itemsPageCount, IEnumerable<BookViewModel> items)
        {
            TotalCount = totalCount;
            ItemsPageCount = itemsPageCount;
            PageCount = (int)Math.Ceiling((double)TotalCount / ItemsPageCount);
            Items = items;
        }
    }
}
