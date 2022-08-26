namespace API.Controllers;

[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;

    public AdminController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(Roles = UsersConstants.Admin)]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithResult()
    {
        var users = await _userManager.Users
            .Include(x => x.UserRoles)
                .ThenInclude(r => r.Role)
            .OrderBy(u => u.UserName)
            .Select(u => new
            {
                u.Id,
                Username = u.UserName,
                Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
            })
            .ToListAsync();

        var users1 = await _userManager.Users
           .Include(r => r.UserRoles)
               .ThenInclude(x => x.Role)
           .ToListAsync();

        return Ok(users);
    }

    [Authorize(Roles = UsersConstants.Moderator)]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(
        string username,
        [FromQuery] string roles)
    {
        var selectedRoles = roles.Split(",").ToArray();

        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return NotFound("User does not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

        if (!result.Succeeded) return BadRequest("Failed to add to roles");

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

        if (!result.Succeeded) return BadRequest("Faild to remove from roles");

        return Ok(await _userManager.GetRolesAsync(user));
    }
}
