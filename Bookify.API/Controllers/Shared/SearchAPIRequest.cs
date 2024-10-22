namespace Bookify.API.Controllers.Shared
{
    public sealed record SearchAPIRequest(int PageNumber, int PageSize, string? Filter, string? SortColumn, string? SortOrder);
}
