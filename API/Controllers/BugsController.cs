namespace API.Controllers;

/// <summary>
/// That Controller is not a part of Producation! 
/// That's just for testing purposes
/// </summary>
[ApiController]
[Route("api/bugs")]
public class BugsController : ControllerBase
{
    private readonly RumbleDbContext _context;

    public BugsController(RumbleDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Try's to access authorized information
    /// </summary>
    /// <returns>will always return 401</returns>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("auth")]
    [Authorize]
    public ActionResult<string> GetSecret()
    {
        return Unauthorized("Unauthorized - Secret information");
    }

    /// <summary>
    /// Try's get not found entity
    /// </summary>
    /// <returns>will always return 404</returns>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("not-found")]
    public ActionResult<UserEntity> GetNotFound()
    {
        var notFoundUser = _context.Users.Find(-1);

        if (notFoundUser == null) return NotFound();

        return Ok(notFoundUser);
    }

    /// <summary>
    /// Get a Bad Request
    /// </summary>
    /// <returns>will always return 400</returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("That's a very very BAD REQUEST");
    }

    /// <summary>
    /// Will get server error
    /// </summary>
    /// <returns>will always return 500</returns>
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var notFoundUser = _context.Users.Find(-1);

        return Ok(notFoundUser!.ToString());
    }

}
