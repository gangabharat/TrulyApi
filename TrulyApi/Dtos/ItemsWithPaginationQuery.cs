namespace TrulyApi.Dtos
{
    public class ItemsWithPaginationQuery
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
