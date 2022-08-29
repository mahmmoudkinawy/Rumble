namespace API.Extensions;
public static class CorsExtensions
{
    public static IServiceCollection AddConfigureCors(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var originsAllowed = configuration.GetSection(CorsConstants.CorsOriginSectionKey)
            .GetChildren()
            .Select(c => c.Value)
            .ToArray();

        if (!originsAllowed.Any()) return services;

        services.AddCors(options =>
        {
            options.AddPolicy(CorsConstants.CorsPolicyName, policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .WithOrigins(originsAllowed);
            });
        });

        return services;
    }
}
