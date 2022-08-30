namespace API.Hubs;
public class MessageHub : Hub
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public MessageHub(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var otherUser = httpContext.Request.Query["user"].ToString();
        var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        var messages = await _messageRepository.GetMessageThreadAsync(Context.User.GetUsername(), otherUser);
        await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
    }

    public async Task SendMessage(CreateMessageDto createMessageDto)
    {
        var username = Context.User.GetUsername();

        if (username == createMessageDto.RecipientUsername.ToLower())
        {
            throw new HubException("Can not send messages to yourself");
        }

        var sender = await _userRepository.GetUserByNameAsync(username);
        var recipient = await _userRepository.GetUserByNameAsync(createMessageDto.RecipientUsername);

        if (recipient == null)
        {
            throw new HubException("Not found user");
        }

        var message = new MessageEntity
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        _messageRepository.AddMessage(message);

        var group = GetGroupName(sender.UserName, recipient.UserName);
        await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));

    }

    private string GetGroupName(string caller, string other)
    {
        var stringComparer = string.CompareOrdinal(caller, other) < 0;

        return stringComparer ? $"{caller}-{other}" : $"{other}-{caller}";
    }
}
