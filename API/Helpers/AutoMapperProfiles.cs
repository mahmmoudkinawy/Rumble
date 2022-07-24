namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserEntity, MemberDto>();
        CreateMap<PhotoEntity, PhotoDto>();
    }
}
