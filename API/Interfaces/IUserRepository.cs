namespace API.Interfaces;
public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetUsersAsync();
    Task<UserEntity> GetUserByIdAsync(int id);
    Task<UserEntity> GetUserByNameAsync(string username);
    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto> GetMemberByUsernameAsync(string username);
    void Update(UserEntity user);

    //I know it's a bad practice to do it like that! because of Single Responsibility
    Task<bool> SaveAllChangeAsync();
}
