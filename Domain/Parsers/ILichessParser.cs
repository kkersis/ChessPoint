using Core.Entities;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Parsers
{
    public interface ILichessParser
    {
        Task<IEnumerable<Game>> ParseGamesAsync(IEnumerable<LichessGame> liGames, string username);
    }
}