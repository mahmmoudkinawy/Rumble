namespace API.Extensions;
public static class ClaimsPrincipleExtenstions
{
    public static string GetUsername(this ClaimsPrincipal claims)
        => claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
