namespace API.Repositories;
public class MessageRepository : IMessageRepository
{
    private readonly RumbleDbContext _context;
    private readonly IMapper _mapper;

    public MessageRepository(RumbleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddMessage(MessageEntity message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMessage(MessageEntity message)
    {
        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
    }

    public async Task<MessageEntity> GetMessageAsync(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams)
    {
        var query = _context.Messages.OrderByDescending(m => m.MessageSent).AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(m => m.Recipient.UserName == messageParams.Username),
            "Outbox" => query.Where(m => m.Sender.UserName == messageParams.Username),
            _ => query.Where(u => u.Recipient.UserName == messageParams.Username &&
                u.DateRead == null)
        };

        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

        return await PagedList<MessageDto>.CreateAsync(
            messages,
            messageParams.PageNumber,
            messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync()
    {
        throw new NotImplementedException();
    }
}
