using AutoMapper;
using Core.Entities.Concrete;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities;
using Entities.Novelty;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthRegisterDTO, Auth>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MailToken, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore()); ; // EF generates ID
            CreateMap<Auth, AuthRegisterDTO > (); // EF generates ID

            CreateMap<AuthLoginDTO, AuthLoginResponseDTO>();
            CreateMap<AuthLoginResponseDTO, AuthLoginDTO>();

            CreateMap<RegulationsAddDTO, Regulations>();
            CreateMap<Regulations, RegulationsAddDTO>();

            CreateMap<TeamMembersAddDTO, TeamMember>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<TeamMember, TeamMembersAddDTO>();
            CreateMap<TeamMembersUpdateDTO, TeamMember>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<TeamMember, TeamMembersUpdateDTO>();

            CreateMap<NoveltyAddDTO, News>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<News, NoveltyAddDTO>();

            CreateMap<NoveltyUpdateDTO, News>();
            CreateMap<News, NoveltyUpdateDTO>();
        }
    }
}
