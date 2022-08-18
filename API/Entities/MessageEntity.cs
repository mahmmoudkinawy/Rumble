namespace API.Entities;
public class MessageEntity
{
    public int Id { get; set; }

    public int SenderId { get; set; }
    public string SenderUsername { get; set; }
    public UserEntity Sender { get; set; }

    public int RecipientId { get; set; }
    public string RecipientUsername { get; set; }
    public UserEntity Recipient { get; set; }

    public string Content { get; set; }
    public DateTime? DateRead { get; set; }
    public DateTime MessageSent { get; set; } = DateTime.UtcNow;//will throw error
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
}
