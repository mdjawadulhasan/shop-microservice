namespace BuildingBlocks.Pagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);


public record PageRequest(int Page = 1, int PageSize = 10);

public class Paginated<T> where T : class
{
    public IReadOnlyList<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public long TotalItems { get; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    private Paginated(IReadOnlyList<T> items, int page, int pageSize, long totalItems)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static async Task<Paginated<T>> CreateAsync(
        IQueryable<T> source,
        PageRequest request,
        CancellationToken cancellationToken = default)
    {
        var totalItems = await source.LongCountAsync(cancellationToken);
        var items = await source
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new Paginated<T>(items, request.Page, request.PageSize, totalItems);
    }
}
