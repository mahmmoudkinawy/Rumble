﻿namespace API.Interfaces;
public interface IMessageRepository
{
    Task AddMessage(MessageEntity message);
    Task DeleteMessage(MessageEntity message);
    Task<MessageEntity> GetMessageAsync(int id);
    Task<PagedList<MessageDto>> GetMessagesForUserAsync();
    Task<IEnumerable<MessageDto>> GetMessageThreadAsync();
}
