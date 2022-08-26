namespace API.DbContexts;
public static class Seed
{
    public static async Task SeedUsers(
        RumbleDbContext context,
        UserManager<UserEntity> userManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userEntityFaker = new Faker<UserEntity>()
           .RuleFor(u => u.UserName, u => u.Internet.UserName().ToLower())
           .RuleFor(u => u.DateOfBirth, u => u.Person.DateOfBirth.ToUniversalTime())
           .RuleFor(u => u.KnownAs, u => u.Internet.UserName())
           .RuleFor(u => u.Gender, u => u.Person.Gender.ToString())
           .RuleFor(u => u.LookingFor, u => u.Lorem.Text())
           .RuleFor(u => u.Interests, u => u.Lorem.Word())
           .RuleFor(u => u.Introduction, u => u.Lorem.Text())
           .RuleFor(u => u.City, u => u.Address.City())
           .RuleFor(u => u.Country, u => u.Address.Country())
           .RuleFor(u => u.PasswordHash,
                u => userManager.PasswordHasher.HashPassword(null, UserConstants.Password));

        var photoEntityFaker = new Faker<PhotoEntity>()
            .RuleFor(p => p.Url, p => p.Person.Avatar)
            .RuleFor(p => p.IsMain, p => true)
            .RuleFor(p => p.UserEntity, u => userEntityFaker);

        foreach (var photoEntity in userEntityFaker.Generate(15))
        {
            context.Photos.Add(photoEntityFaker);
            await context.SaveChangesAsync();
        }
    }
}
