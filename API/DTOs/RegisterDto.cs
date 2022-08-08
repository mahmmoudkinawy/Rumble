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
    /// Put Password that's Required and
    /// Password Max Length is 8 and Minimum is 4, will be changed in the future 
    /// </summary>
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }

    /// <summary>
    /// KnownAs And it's required
    /// </summary>
    [Required]
    public string KnownAs { get; set; }

    /// <summary>
    /// Gender And it's required
    /// </summary>
    [Required]
    public string Gender { get; set; }

    /// <summary>
    /// Date of birth for user
    /// </summary>
    [Required]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// City for the user
    /// </summary>
    [Required]
    public string City { get; set; }

    /// <summary>
    /// Country for the user
    /// </summary>
    [Required]
    public string Country { get; set; }

}
