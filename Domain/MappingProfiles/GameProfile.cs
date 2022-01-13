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
            // chess.com
            CreateMap<ChesscomGame, Game>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.EndTime));
            CreateMap<ChessComPlayer, GamePlayer>();

            // lichess
            CreateMap<LichessGame, Game>();
            CreateMap<LichessPlayer, GamePlayer>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            
        }
    }
}
