namespace API.Repositories;
public class UserRepository : IUserRepository
{
    private readonly RumbleDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(RumbleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberByUsernameAsync(string username)
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(u => u.Username.Equals(username));
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
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
    public async Task<bool> SaveAllChangeAsync()
        => await _context.SaveChangesAsync() > 0;
}
