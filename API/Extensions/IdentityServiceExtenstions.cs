namespace API.Extensions;
public static class IdentityServiceExtenstions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddIdentityCore<UserEntity>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
            .AddRoles<RoleEntity>()
            .AddRoleManager<RoleManager<RoleEntity>>()
            .AddSignInManager<SignInManager<UserEntity>>()
            .AddRoleValidator<RoleValidator<RoleEntity>>()
            .AddEntityFrameworkStores<RumbleDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(UsersConstants.AdminPolicyName, policy => policy.RequireClaim(UsersConstants.Admin));
            options.AddPolicy(UsersConstants.ModeratePolicyName, policy => policy.RequireClaim(UsersConstants.Admin, UsersConstants.ModeratePolicyName));
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]))
                };
            });

        return services;
    }
}
