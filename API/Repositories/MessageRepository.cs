namespace API.Repositories;
public class MessageRepository : IMessageRepository
{
    private readonly RumbleDbContext _context;

    public MessageRepository(RumbleDbContext context)
    {
        _context = context;
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

    public async Task<PagedList<MessageDto>> GetMessagesForUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync()
    {
        throw new NotImplementedException();
    }
}
