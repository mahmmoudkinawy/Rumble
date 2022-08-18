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

    public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(u => u.UserName != userParams.CurrentUsername);
        query = query.Where(u => u.Gender.Equals(userParams.Gender));

        var minDateOfBirth = DateTime.UtcNow.AddYears(-userParams.MaxAge - 1); //-101
        var maxDateOfBirth = DateTime.UtcNow.AddYears(-userParams.MinAge - 1); //14

        query = query.Where(u => u.DateOfBirth <= maxDateOfBirth && u.DateOfBirth >= minDateOfBirth);

        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.Created),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        return await PagedList<MemberDto>.CreateAsync(
            query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(),
            userParams.PageNumber,
            userParams.PageSize);
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

    public async Task<bool> SaveAllChangesAsync()
        => await _context.SaveChangesAsync() > 0;

}
