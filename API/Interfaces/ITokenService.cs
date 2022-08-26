namespace API.Interfaces;
public interface ITokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
}
