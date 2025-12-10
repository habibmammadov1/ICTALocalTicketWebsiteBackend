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

            CreateMap<NoveltyAddDTO, BaseNovelty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<BaseNovelty, NoveltyAddDTO>();

            CreateMap<NoveltyUpdateDTO, BaseNovelty>();
            CreateMap<BaseNovelty, NoveltyUpdateDTO>();

            CreateMap<NoveltyFilesAddDTO, NoveltyFile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<NoveltyFile, NoveltyFilesAddDTO>();

            CreateMap<BaseRulesAddDTO, BaseRules>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<BaseRules, BaseRulesAddDTO>();

            CreateMap<BaseRulesUpdateDTO, BaseRules>();
            CreateMap<BaseRules, BaseRulesUpdateDTO>();

            CreateMap<BaseRulesFilesPhotosAddDTO, BaseRulesFilesPhotos>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CompOfferAddDTO, CompOffer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompOffer, CompOfferAddDTO>();

            CreateMap<CompOfferAnonymAddDTO, CompOffer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CompOffer, CompOfferAnonymAddDTO>();

            CreateMap<MeetingAddDTO, Meeting>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Meeting, MeetingAddDTO>();

            CreateMap<MeetingUpdateDTO, Meeting>();
            CreateMap<Meeting, MeetingUpdateDTO>();
        }
    }
}
