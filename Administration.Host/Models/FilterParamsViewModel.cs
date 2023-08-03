namespace Administration.Host.Models
{
    public class FilterParamsViewModel
    {
        private int _page;
        private int _pageCount;

        public int? Page
        {
            get => _page;
            set => _page = value.HasValue
                ? value.Value == 0
                   ? 1
                   : value.Value
                : 1;
        }
        public int? PageCount
        {
            get => _pageCount;
            set => _pageCount = value.HasValue
                ? value.Value == 0
                   ? int.MaxValue
                   : value.Value
                : int.MaxValue;
        }

        public int Index => Page.Value - 1;
    }
}
