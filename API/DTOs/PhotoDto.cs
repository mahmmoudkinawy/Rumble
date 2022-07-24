namespace API.DTOs;

/// <summary>
/// Photo DTO with specific attributes
/// </summary>
public class PhotoDto
{
    public int Id { get; set; }

    /// <summary>
    /// URL of the hosted image for this user
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Indicator that return if that's the main profile Photo or not
    /// </summary>
    public bool IsMain { get; set; }
}
