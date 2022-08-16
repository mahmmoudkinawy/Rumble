namespace API.Interfaces;
public interface ILikesRepository
{
    Task<UserLikeEntity> GetUserLike(int sourceUserId, int likedUserId);
    Task<UserEntity> GetUserWithLikes(int userId);
    Task<IReadOnlyList<LikeDto>> GetUserLikes(string predicate, int userId);
}
