namespace API.Controllers;

[Route("api/messages")]
[ApiController]
[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public MessagesController(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageDto createMessageDto)
    {
        var username = User.GetUsername();

        if (username.Equals(createMessageDto.RecipientUsername))
        {
            return BadRequest("You can not sent message to yourself");
        }

        var sender = await _userRepository.GetUserByNameAsync(username);
        var recipient = await _userRepository.GetUserByNameAsync(createMessageDto.RecipientUsername);

        if (recipient == null)
        {
            return NotFound("User was not found");
        }

        var message = new MessageEntity
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        await _messageRepository.AddMessage(message);

        return Ok(_mapper.Map<MessageDto>(message));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageForUser(
        [FromQuery] MessageParams messageParams)
    {
        messageParams.Username = User.GetUsername();

        var messages = await _messageRepository.GetMessagesForUserAsync(messageParams);

        Response.AddPaginationHeader(
            messages.CurrentPage,
            messages.PageSize,
            messages.TotalPages,
            messages.TotalCount);

        return Ok(messages);
    }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread([FromRoute] string username)
    {
        var currentUsername = User.GetUsername();

        return Ok(await _messageRepository.GetMessageThreadAsync(currentUsername, username));
    }
}
