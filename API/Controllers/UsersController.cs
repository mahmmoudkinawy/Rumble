namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly RumbleDbContext _context;

    public UsersController(RumbleDbContext context)
        => _context = context;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserEntity>> GetUsers()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserEntity>> GetUser([FromRoute] int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null ? Ok(user) : NotFound();
    }


}
