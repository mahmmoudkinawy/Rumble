namespace API.DbContexts;
public class RumbleDbContext : DbContext
{
    public RumbleDbContext(DbContextOptions<RumbleDbContext> options) : base(options)
    { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PhotoEntity> Photos { get; set; }
}
