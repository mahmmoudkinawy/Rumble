namespace API.Extensions;
public static class HttpExtenstions
{
    public static void AddPaginationHeader(this HttpResponse response,
        int currentPage,
        int itemsPerPage,
        int totalPages,
        int totalItems)
    {
        var paginationHeader = new PaginationHeader(currentPage,
            itemsPerPage, 
            totalPages,
            totalItems);

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}
