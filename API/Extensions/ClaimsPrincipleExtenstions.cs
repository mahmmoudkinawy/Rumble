namespace API.Extensions;
public static class ClaimsPrincipleExtenstions
{
    public static string GetUsername(this ClaimsPrincipal claims)
        => claims.FindFirst(ClaimTypes.Name)?.Value;

    public static int GetUserById(this ClaimsPrincipal claims)
        => int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
