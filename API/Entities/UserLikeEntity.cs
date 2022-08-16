namespace API.Entities;

[Table("UserLike")]
public class UserLikeEntity
{
    public UserEntity SourceUser { get; set; }
    public int SourceUserId { get; set; }

    public UserEntity LikedUser { get; set; }
    public int LikedUserId { get; set; }
}
