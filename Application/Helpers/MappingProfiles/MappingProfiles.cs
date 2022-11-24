using Application.Dto;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostDto>()
            .ForMember(dto => dto.UserName,
                expression=>
                    expression.MapFrom(post=> post.User.UserName));
       CreateMap<User, UserDto>();
    }
}