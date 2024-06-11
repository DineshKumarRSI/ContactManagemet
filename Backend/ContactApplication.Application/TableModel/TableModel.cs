namespace ContactApplication.Application
{
    public class TableModel
    {
        public string? Search { get; set; }
        public Sorting? Sorting { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class Sorting
    {
        public string ColumnName { get; set; }
        public Order Order { get; set; }
    }

    public enum Order
    {
        Ascending = 1,
        Descending = 2,
    }
}
