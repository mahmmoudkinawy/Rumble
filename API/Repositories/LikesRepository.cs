namespace API.Repositories;
public class LikesRepository : ILikesRepository
{
    private readonly RumbleDbContext _context;

    public LikesRepository(RumbleDbContext context)
    {
        _context = context;
    }

    public async Task<UserLikeEntity> GetUserLike(int sourceUserId, int likedUserId)
    {
        return await _context.Likes.FindAsync(sourceUserId, likedUserId);
    }

    public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
    {
        var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
        var likes = _context.Likes.AsQueryable();

        if (likesParams.Predicate == "liked")
        {
            likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
            users = likes.Select(like => like.LikedUser);
        }

        if (likesParams.Predicate == "likedBy")
        {
            likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
            users = likes.Select(like => like.SourceUser);
        }

        var likedUsers = users.Select(user => new LikeDto
        {
            Id = user.Id,
            Age = user.DateOfBirth.CalculateAge(),
            KnownAs = user.KnownAs,
            Username = user.UserName,
            City = user.City,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url
        });

        return await PagedList<LikeDto>.CreateAsync(
            likedUsers,
            likesParams.PageNumber,
            likesParams.PageSize);
    }

    public async Task<UserEntity> GetUserWithLikes(int userId)
    {
        return await _context.Users
            .Include(l => l.LikedUsers)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
