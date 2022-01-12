using AutoMapper;
using Core;
using Core.Entities;
using Core.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Parsers
{
    public class LichessParser : ILichessParser
    {
        private readonly ApiSettings _api;
        private readonly IMapper _mapper;

        public LichessParser(ApiSettings api, IMapper mapper)
        {
            _api = api;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Game>> ParseGamesAsync(IEnumerable<LichessGame> liGames, string username)
        {
            return await Task.Run(() => ParseGames(liGames, username));
        }

        private IEnumerable<Game> ParseGames(IEnumerable<LichessGame> liGames, string username)
        {
            var games = new List<Game>();
            foreach (var liGame in liGames)
            {
                if (liGame.Players.White.AiLevel != null || liGame.Players.Black.AiLevel != null) continue;
                if (liGame.Variant != "standard") continue;
                if (liGame.Opening == null) continue;

                var game = _mapper.Map<Game>(liGame);
                game.Url = _api.LichessSite + liGame.Id;
                game.Date = ConvertJsonTimeToDateTime(liGame.TimeInMiliSec);
                game.MyColor = FindMyColor(liGame, username);
                game.Ending = FindEnding(liGame, game.MyColor);
                game.Me = game.MyColor == ColorType.WHITE ? _mapper.Map<GamePlayer>(liGame.Players.White) : _mapper.Map<GamePlayer>(liGame.Players.Black);
                game.Opponent = game.MyColor == ColorType.WHITE ? _mapper.Map<GamePlayer>(liGame.Players.Black) : _mapper.Map<GamePlayer>(liGame.Players.White);
                games.Add(game);
            }
            return games;
        }

        // this is needed because newtonsoft json converter can't convert when timestamp is in milliseconds instead of seconds
        private static DateTime ConvertJsonTimeToDateTime(long timeInMiliSec)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timeInMiliSec);
        }

        private static ColorType FindMyColor(LichessGame liGame, string username)
        {
            if (liGame.Players.White.User.Username == username)
                return ColorType.WHITE;
            else return ColorType.BLACK;
        }

        private static EndingType FindEnding(LichessGame liGame, ColorType myColor)
        {
            if (liGame.Winner == null)
                return EndingType.DRAW;
            else if (liGame.Winner == "white")
            {
                if (myColor == ColorType.WHITE)
                    return EndingType.WIN;
                else return EndingType.LOSE;
            }
            else
            {
                if (myColor == ColorType.BLACK)
                    return EndingType.WIN;
                else return EndingType.LOSE;
            }
        }
    }
}
