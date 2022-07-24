namespace API.Repositories;
public class UserRepository : IUserRepository
{
    private readonly RumbleDbContext _context;

    public UserRepository(RumbleDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UserEntity> GetUserByNameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
    }

    public void Update(UserEntity user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
}
