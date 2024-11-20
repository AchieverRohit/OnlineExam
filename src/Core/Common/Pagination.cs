namespace thinkschool.OnlineExam.Core.Common;
public class BasePagination
{
    public Pagination? Options { get; set; }
}
public class Pagination
{
    public int PageNum { get; set; } = -1; // Disable pagination by default
    public int PageSize { get; set; } = 100;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}
public class PageDetails(int pageNum, int pageSize, int totalCount)
{
    public int PageNumber { get; } = pageNum;
    public int PageSize { get; } = pageSize;
    public int TotalPages { get; } = pageNum <= 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);
    public int TotalCount { get; } = totalCount;
    public bool HasPreviousPage => TotalPages > 1 && PageNumber > 1;
    public bool HasNextPage => TotalPages > 1 && PageNumber < TotalPages;
}
