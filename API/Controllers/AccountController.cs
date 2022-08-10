using System.Security.Cryptography;

namespace API.Controllers;

/// <summary>
/// Account Controller for Authentication the users
/// </summary>
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly RumbleDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(
        RumbleDbContext context,
        ITokenService tokenService,
        IMapper mapper)
    {
        _context = context;
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
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        var user = _mapper.Map<UserEntity>(registerDto);

        using var hmac = new HMACSHA512();

        user.UserName = registerDto.Username.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new UserDto
        {
            Token = _tokenService.CreateToken(user),
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
        var user = await _context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Username does not found");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid password");
            }
        }

        return Ok(new UserDto
        {
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
            PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        });
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
    }

}
