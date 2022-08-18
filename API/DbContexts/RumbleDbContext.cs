namespace API.DbContexts;
public class RumbleDbContext : DbContext
{
    public RumbleDbContext(DbContextOptions<RumbleDbContext> options) : base(options)
    { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PhotoEntity> Photos { get; set; }
    public DbSet<UserLikeEntity> Likes { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserLikeEntity>()
            .HasKey(k => new { k.SourceUserId, k.LikedUserId });

        modelBuilder.Entity<UserLikeEntity>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.LikedUsers)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserLikeEntity>()
            .HasOne(s => s.LikedUser)
            .WithMany(l => l.LikedByUsers)
            .HasForeignKey(s => s.LikedUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MessageEntity>()
            .HasOne(m => m.Sender)
            .WithMany(m => m.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MessageEntity>()
            .HasOne(m => m.Recipient)
            .WithMany(m => m.MessagesReceived)
            .HasForeignKey(m => m.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}
