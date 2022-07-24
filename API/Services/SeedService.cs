using System.Security.Cryptography;

namespace API.Services;
public class SeedService : ISeedService
{
    private readonly RumbleDbContext _context;

    public SeedService(RumbleDbContext context)
    {
        _context = context;
    }

    public async Task SeedData()
    {
        if (await _context.Users.AnyAsync()) return;

        using var hmac = new HMACSHA512();

        var userEntityFaker = new Faker<UserEntity>()
            .RuleFor(u => u.UserName, u => u.Person.UserName)
            .RuleFor(u => u.DateOfBirth, u => u.Person.DateOfBirth.ToUniversalTime())
            .RuleFor(u => u.KnownAs, u => u.Person.UserName.ToLower())
            .RuleFor(u => u.Gender, u => u.Person.Gender.ToString())
            .RuleFor(u => u.LookingFor, u => u.Lorem.Text())
            .RuleFor(u => u.Interests, u => u.Lorem.Word())
            .RuleFor(u => u.Introduction, u => u.Lorem.Text())
            .RuleFor(u => u.City, u => u.Address.City())
            .RuleFor(u => u.Country, u => u.Address.Country())
            .RuleFor(u => u.PasswordHash, u => hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")))
            .RuleFor(u => u.PasswordSalt, u => hmac.Key);

        var photoFakerBogus = new Faker<PhotoEntity>()
            .RuleFor(p => p.Url, p => p.Person.Avatar)
            .RuleFor(p => p.IsMain, p => true)
            .RuleFor(p => p.UserEntity, u => userEntityFaker);

        foreach (var photoFaker in photoFakerBogus.Generate(1000))
        {
            _context.Photos.Add(photoFaker);
            await _context.SaveChangesAsync();
        }

    }
}
