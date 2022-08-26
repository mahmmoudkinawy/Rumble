namespace API.Entities;
public class UserEntity : IdentityUser<int>
{
    public DateTime DateOfBirth { get; set; }
    public string? KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string? Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    public ICollection<PhotoEntity> Photos { get; set; }

    public ICollection<UserLikeEntity> LikedByUsers { get; set; }
    public ICollection<UserLikeEntity> LikedUsers { get; set; }

    public ICollection<MessageEntity> MessagesSent { get; set; }
    public ICollection<MessageEntity> MessagesReceived { get; set; }

    public ICollection<UserRoleEntity> UserRoles { get; set; }
}
