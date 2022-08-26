namespace API.Entities;
public class RoleEntity : IdentityRole<int>
{
    public ICollection<UserRoleEntity> UserRoles { get; set; }
}
