namespace API.Helpers;
public class UserParams
{
    private const int _maxPageSize = 50;
    public int PageNumber { get; init; } = 1;
    public int _pageSize { get; set; } = 5;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
    }
}
