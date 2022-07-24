namespace API.Interfaces;
public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetUsersAsync();
    Task<UserEntity> GetUserByIdAsync(int id);
    Task<UserEntity> GetUserByNameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberByUsernameAsync(string username);
    void Update(UserEntity user);
}
