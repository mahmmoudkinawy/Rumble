namespace API.Extensions;
public static class ApplicationServiceExtenstions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSingleton<PresenceTracker>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddScoped<ILikesRepository, LikesRepository>();

        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<LogUserActivity>();

        services.AddSignalR();

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers(options => options.Filters.Add(new LogUserActivity()))
            .AddXmlSerializerFormatters();

        services.AddDbContext<RumbleDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
