namespace API.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LikeDto>>> GetUserLikes(string predicate)
    {
        var users = await _likesRepository.GetUserLikes(predicate, User.GetUserById());

        return Ok(users);
    }
}
