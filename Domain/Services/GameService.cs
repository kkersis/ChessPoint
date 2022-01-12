using AutoMapper;
using Core;
using Core.Entities;
using DataAccess.Repositories.ApiCallers;
using Domain.Models;
using Domain.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IChesscomRepository _chesscomRepository;
        private readonly ILichessRepository _lichessRepository;
        private readonly IChesscomParser _chesscomParser;
        private readonly ILichessParser _lichessParser;

        public GameService(IChesscomRepository chessComRepository, ILichessRepository lichessRepository,
                           IChesscomParser chesscomParser, ILichessParser lichessParser)
        {
            _chesscomRepository = chessComRepository;
            _lichessRepository = lichessRepository;
            _chesscomParser = chesscomParser;
            _lichessParser = lichessParser;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(string chesscomusr, string lichessusr)
        {
            //TODO: implement asynchronous games getting from both sites at the same time
            var chesscomGames = await GetAllChesscomAsync(chesscomusr);
            var lichessGames = await GetAllLichessAsync(lichessusr);
            var games = chesscomGames.Concat(lichessGames);
            return games;
        }

        public async Task<IEnumerable<Game>> GetAllChesscomAsync(string username)
        {
            var chesscomGames = await _chesscomRepository.GetAllChesscomAsync(username);
            return await _chesscomParser.ParseGamesAsync(chesscomGames, username);
        }

        public async Task<IEnumerable<Game>> GetAllLichessAsync(string username)
        {
            var lichessGames = await _lichessRepository.GetAllLichessAsync(username);
            return await _lichessParser.ParseGamesAsync(lichessGames, username);
        }
    }
}
