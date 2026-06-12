using Microsoft.EntityFrameworkCore;
using SharedKernal.Abstract.Shared.Collections.Pagination;

namespace SharedKernal.Impl.Shared.Collections.Pagination;

public class PaginationService : IPaginationService
{
    public async Task<IPagedList<T>> CreateAsync<T>(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        // 1. Sanitize input to prevent database faults or logical overflows
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 10 : pageSize;

        // 2. Fetch the total count from a database before clipping chunks
        int totalCount = await source.CountAsync(ct);

        // 3. Fallback check: If the database is completely empty, don't execute a skip query
        if (totalCount == 0)
        {
            return new PagedList<T>(Array.Empty<T>(), 0, pageNumber, pageSize);
        }

        // 4. Calculate offset and extract page partition from the database
        int skipCount = (pageNumber - 1) * pageSize;
        
        List<T> items = await source
            .Skip(skipCount)
            .Take(pageSize)
            .ToListAsync(ct);

        // 5. Wrap inside the immutable result envelope
        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }
}