using AutoMapper;
using Core.Entities;
using Core.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Parsers
{
    public class ChesscomParser : IChesscomParser
    {
        private readonly IMapper _mapper;

        public ChesscomParser(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<Game>> ParseGamesAsync(IEnumerable<ChesscomGame> chesscomGames, string username)
        {
            return await Task.Run(() => ParseGames(chesscomGames, username));
        }

        private IEnumerable<Game> ParseGames(IEnumerable<ChesscomGame> chesscomGames, string username)
        {
            Task<OpeningInfo>[] openingTasks = new Task<OpeningInfo>[chesscomGames.Count()];

            for (int i = 0; i < chesscomGames.Count(); i++)
            {
                openingTasks[i] = GetOpeningInfoAsync(chesscomGames.ElementAt(i).Pgn);
            }

            var games = new List<Game>();
            Task.WaitAll(openingTasks);

            for (int i = 0; i < chesscomGames.Count(); i++)
            {
                var chesscomGame = chesscomGames.ElementAt(i);
                var game = _mapper.Map<Game>(chesscomGame);
                games.Add(game);
                games.ElementAt(i).Opening = openingTasks[i].Result;
                games.ElementAt(i).Ending = FindEnding(chesscomGame, username);
                games.ElementAt(i).MyColor = FindMyColor(chesscomGame, username);
            }

            return games;
        }

        private static async Task<OpeningInfo> GetOpeningInfoAsync(string pgn)
        {
            return await Task.Run(() => GetOpeningInfo(pgn));
        }

        // gets opening info from chess.com pgn string
        private static OpeningInfo GetOpeningInfo(string pgn)
        {
            string[] tLines = pgn.Split('\n');
            string eco;
            string name;

            try
            {
                eco = tLines[9].Substring(6, 3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                eco = null;
            }

            try
            {
                name = tLines[10][40..^2];
                name = name.Replace('-', ' ');
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                name = null;
            }

            return new OpeningInfo { Eco = eco, Name = name };
        }

        private static EndingType FindEnding(ChesscomGame game, string username)
        {
            if (game.WhitePlayer.Username == username)
            {
                if (game.WhitePlayer.Result == "win")
                    return EndingType.WIN;
                if (game.BlackPlayer.Result == "win")
                    return EndingType.LOSE;
                return EndingType.DRAW;
            }
            else
            {
                if (game.WhitePlayer.Result == "win")
                    return EndingType.LOSE;
                if (game.BlackPlayer.Result == "win")
                    return EndingType.WIN;
                return EndingType.DRAW;
            }
        }

        private static ColorType FindMyColor(ChesscomGame game, string username)
        {
            if (game.WhitePlayer.Username == username)
                return ColorType.WHITE;
            else
                return ColorType.BLACK;
        }
    }
}
