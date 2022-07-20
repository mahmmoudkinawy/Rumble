namespace API.Extensions;
public static class ApplicationServiceExtenstions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddControllers().AddXmlSerializerFormatters();

        services.AddDbContext<RumbleDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}
