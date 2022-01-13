using AutoMapper;
using Core.Entities;
using Core.Enums;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Parsers
{
    public class ChesscomParser : IChesscomParser
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ChesscomParser> _logger;

        public ChesscomParser(IMapper mapper, ILogger<ChesscomParser> logger)
        {
            _mapper = mapper;
            _logger = logger;
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
                game.Opening = openingTasks[i].Result;
                game.Ending = FindEnding(chesscomGame, username);
                game.MyColor = FindMyColor(chesscomGame, username);
                game.Me = game.MyColor == ColorType.WHITE ?
                    ConvertToGamePlayer(chesscomGame.WhitePlayer) :
                    ConvertToGamePlayer(chesscomGame.BlackPlayer);
                game.Opponent = game.MyColor == ColorType.WHITE ?
                    ConvertToGamePlayer(chesscomGame.BlackPlayer) :
                    ConvertToGamePlayer(chesscomGame.WhitePlayer);
                games.Add(game);
            }

            return games;
        }

        private async Task<OpeningInfo> GetOpeningInfoAsync(string pgn)
        {
            return await Task.Run(() => GetOpeningInfo(pgn));
        }

        private GamePlayer ConvertToGamePlayer(ChessComPlayer player)
        {
            return _mapper.Map<GamePlayer>(player);
        }

        // gets opening info from chess.com pgn string
        private OpeningInfo GetOpeningInfo(string pgn)
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
                _logger.LogError(e.StackTrace);
                eco = null;
            }

            try
            {
                name = tLines[10][40..^2];
                name = name.Replace('-', ' ');
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
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
