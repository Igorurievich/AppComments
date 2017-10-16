using App.Comments.Common.Entities;
using AutoMapper;

namespace App.Comments.Common.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Comment, CommentDto>()
			.ForMember(dest => dest.Autor, opt => opt.MapFrom(source => source.ApplicationUser.UserName));
		}
	}
}
