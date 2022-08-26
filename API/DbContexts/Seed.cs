namespace API.DbContexts;
public static class Seed
{
    public static async Task SeedUsers(
        RumbleDbContext context,
        UserManager<UserEntity> userManager,
        RoleManager<RoleEntity> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<RoleEntity>
        {
            new RoleEntity { Name = UsersConstants.Moderator },
            new RoleEntity { Name = UsersConstants.Admin },
            new RoleEntity { Name = UsersConstants.Member }
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        var userEntityFaker = new Faker<UserEntity>()
           .RuleFor(u => u.UserName, u => u.Internet.UserName().ToLower())
           .RuleFor(u => u.DateOfBirth, u => u.Person.DateOfBirth.ToUniversalTime())
           .RuleFor(u => u.KnownAs, u => u.Internet.UserName())
           .RuleFor(u => u.Gender, u => u.Person.Gender.ToString())
           .RuleFor(u => u.LookingFor, u => u.Lorem.Text())
           .RuleFor(u => u.Interests, u => u.Lorem.Word())
           .RuleFor(u => u.Introduction, u => u.Lorem.Text())
           .RuleFor(u => u.City, u => u.Address.City())
           .RuleFor(u => u.Country, u => u.Address.Country());

        foreach (var userEntity in userEntityFaker.Generate(15))
        {
            await userManager.CreateAsync(userEntity, UsersConstants.Password);
            await userManager.AddToRoleAsync(userEntity, UsersConstants.Member);
        }

        for (int i = 1; i <= 15; i++)
        {
            var photoEntityFaker = new Faker<PhotoEntity>()
                .RuleFor(p => p.Url, p => p.Person.Avatar)
                .RuleFor(p => p.IsMain, p => true)
                .RuleFor(p => p.UserId, p => i);

            context.Photos.Add(photoEntityFaker);
            await context.SaveChangesAsync();
        }

        var admin = new UserEntity
        {
            UserName = "admin"
        };

        await userManager.CreateAsync(admin, UsersConstants.Password);
        await userManager.AddToRolesAsync(admin, new[] { UsersConstants.Admin, UsersConstants.Member });
    }
}
