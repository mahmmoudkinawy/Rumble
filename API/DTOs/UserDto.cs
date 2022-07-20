namespace API.DTOs;

/// <summary>
/// UserDto is the model the returns from the two end points => register and login for authenticating the users
/// </summary>
public class UserDto
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Username as a string
    /// </summary>
    public string Username { get; set; }
}
