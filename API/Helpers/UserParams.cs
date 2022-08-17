namespace API.Helpers;
public class UserParams : PaginationParams
{
    public string? CurrentUsername { get; set; }
    public string? Gender { get; set; }

    public int MinAge { get; init; } = 15;
    public int MaxAge { get; init; } = 100;

    public string OrderBy { get; init; } = "lastActive";
}
