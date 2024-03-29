﻿namespace API.Controllers;

/// <summary>
/// Likes Controller for dealing with likes!!
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LikesController : ControllerBase
{
    private readonly ILikesRepository _likesRepository;
    private readonly IUserRepository _userRepository;

    public LikesController(ILikesRepository likesRepository, IUserRepository userRepository)
    {
        _likesRepository = likesRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// This is end point add a like to a particular user using username!
    /// </summary>
    /// <param name="username"></param>
    /// <returns>Nothing</returns>
    [HttpPost("{username}")]
    public async Task<ActionResult> AddLike([FromRoute] string username)
    {
        var sourceUserId = User.GetUserById();
        var likedUser = await _userRepository.GetUserByNameAsync(username);
        var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

        if (likedUser == null) return NotFound();

        if (sourceUser.UserName == username) return BadRequest("Can not like yourself");

        var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

        if (userLike != null) return BadRequest("Already liked this user");

        userLike = new UserLikeEntity
        {
            SourceUserId = sourceUserId,
            LikedUserId = likedUser.Id
        };

        sourceUser.LikedUsers.Add(userLike);

        if (await _userRepository.SaveAllChangesAsync()) return Ok();

        return BadRequest("Problem to like the User");
    }

    /// <summary>
    /// Returns all likes based on the logged in user!
    /// </summary>
    /// <param name="predicate">liked or likedBy</param>
    /// <returns>Pagination likes! look at the headers of the returned request</returns>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserById();

        var users = await _likesRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(
            users.CurrentPage,
            users.PageSize, 
            users.TotalPages,
            users.TotalCount);

        return Ok(users);
    }
}
