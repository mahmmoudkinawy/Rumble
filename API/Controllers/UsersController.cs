namespace API.Controllers;

/// <summary>
/// Users Controller for getting users details
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Returns all the users in the database
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserEntity>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }

    /// <summary>
    /// Returns a particular user with the given id
    /// </summary>
    /// <param name="id">Id must be passed as a Route in the API</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserEntity>> GetUser([FromRoute] int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return user != null ? Ok(user) : NotFound();
    }


}
