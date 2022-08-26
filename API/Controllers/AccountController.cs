namespace API.Controllers;

/// <summary>
/// Account Controller for Authentication the users
/// </summary>
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    /// <summary>
    /// Register endpoint for registering a new user with new email
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns>Returns User Dto that returns token and username</returns>

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(u => u.UserName.Equals(registerDto.Username)))
            return BadRequest("Username is taken");

        var user = _mapper.Map<UserEntity>(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleList = await _userManager.AddToRoleAsync(user, UsersConstants.Member);

        if (!roleList.Succeeded) return BadRequest(result.Errors);

        return Ok(new UserDto
        {
            Token = await _tokenService.CreateTokenAsync(user),
            Username = user.UserName,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        });
    }

    /// <summary>
    /// Login endpoint for log the user in with username and password
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns>Returns User Dto that returns token and username</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.UserName.Equals(loginDto.Username));

        if (user == null) return Unauthorized("Username does not found");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized();

        return Ok(new UserDto
        {
            Token = await _tokenService.CreateTokenAsync(user),
            Username = user.UserName,
            PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        });
    }

}
