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
}
