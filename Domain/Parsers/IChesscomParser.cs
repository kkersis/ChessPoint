using Core.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Parsers
{
    public interface IChesscomParser
    {
        Task<IEnumerable<Game>> ParseGamesAsync(IEnumerable<ChesscomGame> chesscomGames, string username);
    }
}