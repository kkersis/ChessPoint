using AutoMapper;
using Core.Entities;
using Core.Enums;
using Domain.Models;

namespace Domain.MappingProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<ChessComPlayer, GamePlayer>();
            CreateMap<LichessPlayer, GamePlayer>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<LichessGame, Game>();
            CreateMap<ChesscomGame, Game>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Me, opt =>
                        opt.MapFrom(src => src.MyColor == ColorType.WHITE ? src.WhitePlayer : src.BlackPlayer))
                .ForMember(dest => dest.Opponent, opt =>
                        opt.MapFrom(src => src.MyColor == ColorType.WHITE ? src.BlackPlayer : src.WhitePlayer));
        }
    }
}
