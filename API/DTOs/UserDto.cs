﻿namespace API.DTOs;

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

    /// <summary>
    /// Image Url as a link
    /// </summary>
    public string PhotoUrl { get; set; }

    /// <summary>
    /// Knows as to be displayed in the nav bar
    /// </summary>
    public string KnownAs { get; set; }

    /// <summary>
    /// Gender of the activited user
    /// </summary>
    public string Gender { get; set; }
}
