namespace API.Controllers;

/// <summary>
/// Users Controller for getting users details
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns all the users in the database
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberDto>> GetUsers()
    {
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(await _userRepository.GetMembersAsync()));
    }

    /// <summary>
    /// Returns a particular user with the given id
    /// </summary>
    /// <param name="id">Id must be passed as a Route in the API</param>
    /// <returns></returns>
    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> GetUser([FromRoute] string username)
    {
        var user = await _userRepository.GetMemberByUsernameAsync(username);

        return user != null ? Ok(_mapper.Map<MemberDto>(user)) : NotFound();
    }


}
