namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserEntity, MemberDto>()
            .ForMember(d => d.Age, o => o.MapFrom(u => u.DateOfBirth.CalculateAge()))
            .ForMember(d => d.PhotoUrl, o => o.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url));
        
        CreateMap<PhotoEntity, PhotoDto>();

        CreateMap<MemberUpdateDto, UserEntity>();

        CreateMap<RegisterDto, UserEntity>().ReverseMap();
    }
}
