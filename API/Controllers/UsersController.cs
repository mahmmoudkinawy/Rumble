namespace API.Controllers;

/// <summary>
/// Users Controller for getting users details
/// </summary>
[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoService _photoService;
    private readonly IMapper _mapper;

    public UsersController(
        IUserRepository userRepository,
        IPhotoService photoService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _photoService = photoService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns all the users in the database
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberDto>> GetUsers([FromQuery] UserParams userParams)
    {
        var user = await _userRepository.GetUserByNameAsync(User.GetUsername());
        userParams.CurrentUsername = user.UserName;

        if (string.IsNullOrEmpty(userParams.Gender))
            userParams.Gender = user.Gender == "Male" ? "Female" : "Male";

        var users = await _userRepository.GetMembersAsync(userParams);

        Response.AddPaginationHeader(
            users.CurrentPage,
            users.PageSize,
            users.TotalPages,
            users.TotalCount);

        return Ok(users);
    }

    /// <summary>
    /// Returns a particular user with the given id
    /// </summary>
    /// <param name="id">Id must be passed as a Route in the API</param>
    /// <returns></returns>
    [HttpGet("{username}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> GetUser([FromRoute] string username)
    {
        var user = await _userRepository.GetMemberByUsernameAsync(username);

        return user != null ? Ok(user) : NotFound();
    }

    /// <summary>
    /// Update some attributes for the user
    /// </summary>
    /// <param name="memberUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser([FromBody] MemberUpdateDto memberUpdateDto)
    {
        var user = await _userRepository.GetUserByNameAsync(User.GetUsername());

        _mapper.Map(memberUpdateDto, user);

        _userRepository.Update(user);

        if (await _userRepository.SaveAllChangesAsync()) return NoContent();

        return BadRequest("Problem Updating User");
    }

    /// <summary>
    /// Add an Profile Image! and if there none a profile images at all!
    /// The first image gonna be a Main Image
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> UploadImage(IFormFile file)
    {
        var user = await _userRepository.GetUserByNameAsync(User.GetUsername());

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new PhotoEntity
        {
            Url = result.Url.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllChangesAsync())
            return CreatedAtRoute(
                nameof(GetUser),
                new { username = user.UserName },
                _mapper.Map<PhotoDto>(photo));

        return BadRequest("Problem Uploading Image");
    }

    /// <summary>
    /// Set another image as the main one! instead of the main image!
    /// </summary>
    /// <param name="photoId">Photo Id to be set main photo</param>
    /// <returns>Returns an object</returns>

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<IActionResult> SetMainPhoto([FromRoute] int photoId)
    {
        var user = await _userRepository.GetUserByNameAsync(User.GetUsername());

        var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

        if (photo == null) return NotFound("There is no image with this ID");

        if (photo.IsMain) return BadRequest("Already the main photo");

        var currentPhoto = user.Photos.FirstOrDefault(p => p.IsMain);

        if (currentPhoto != null)
        {
            currentPhoto.IsMain = false;
            photo.IsMain = true;

            await _userRepository.SaveAllChangesAsync();
            return NoContent();
        }

        return BadRequest("Problem Setting the main photo");
    }

    /// <summary>
    /// Delete an image from the images collection, and if it was the main one! 
    /// It won't be deleted
    /// </summary>
    /// <param name="photoId">Photo Id to be deleted</param>
    /// <returns>Returns an object</returns>

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto([FromRoute] int photoId)
    {
        var user = await _userRepository.GetUserByNameAsync(User.GetUsername());

        var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

        if (photo == null) return NotFound("Image with the given ID was not found");

        if (photo.IsMain) return BadRequest("Can not delete main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);

            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await _userRepository.SaveAllChangesAsync()) return Ok();

        return BadRequest("Problem deleting the photo");
    }
}
