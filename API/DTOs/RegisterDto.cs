namespace API.DTOs;

/// <summary>
/// Model that used by account controller for register users
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Put Username and it's Required
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Put Password and it's Required
    /// </summary>
    [Required]
    public string Password { get; set; }
}
