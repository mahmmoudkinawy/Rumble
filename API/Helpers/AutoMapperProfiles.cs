namespace API.Helpers;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserEntity, MemberDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(u => u.DateOfBirth.CalculateAge()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(u => u.Photos.FirstOrDefault(p => p.IsMain).Url));
        
        CreateMap<PhotoEntity, PhotoDto>();

        CreateMap<MemberUpdateDto, UserEntity>();

        CreateMap<RegisterDto, UserEntity>().ReverseMap();

        CreateMap<MessageEntity, MessageDto>()
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
    }
}
