namespace API.Interfaces;
public interface ILikesRepository
{
    Task<UserLikeEntity> GetUserLike(int sourceUserId, int likedUserId);
    Task<UserEntity> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
}
