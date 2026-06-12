namespace SharedKernal.Abstract.Shared.Collections.Pagination;

public interface IPagedList<T>
{
    IReadOnlyList<T> Items { get; }
    
    int PageNumber { get; }
    int PageSize { get; }
    int NumberOfElements => Items.Count; 
    int TotalCount { get; }
    int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    bool HasPreviousPage => PageNumber > 1;
    bool HasNextPage => PageNumber < TotalPages;
    bool IsFirstPage => PageNumber == 1;
    bool IsLastPage => PageNumber >= TotalPages;
    bool IsEmpty => !Items.Any();
}


