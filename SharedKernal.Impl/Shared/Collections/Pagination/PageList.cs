using Microsoft.EntityFrameworkCore;
using SharedKernal.Abstract.Shared.Collections.Pagination;

namespace SharedKernal.Impl.Shared.Collections.Pagination;

public class PagedList<T> : IPagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public PagedList(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : pageSize; // Safeguard against divide-by-zero or negatives
    }
    
}


