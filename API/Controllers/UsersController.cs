﻿using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly RumbleDbContext _context;

    public UsersController(RumbleDbContext context)
        => _context = context;

    /// <summary>
    /// Returns all the users in the database
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserEntity>> GetUsers()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    /// <summary>
    /// Returns a particular user with the given id
    /// </summary>
    /// <param name="id">Id must be passed as a Route in the API</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserEntity>> GetUser([FromRoute] int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user != null ? Ok(user) : NotFound();
    }


}
