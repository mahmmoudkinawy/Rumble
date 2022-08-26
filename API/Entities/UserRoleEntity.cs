namespace API.Entities;
public class UserRoleEntity : IdentityUserRole<int>
{
    public UserEntity User { get; set; }
    public RoleEntity Role { get; set; }
}
