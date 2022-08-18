namespace API.DTOs;
public class CreateMessageDto
{
    public string RecipientUsername { get; set; }

    [Required]
    public string Content { get; set; }
}
