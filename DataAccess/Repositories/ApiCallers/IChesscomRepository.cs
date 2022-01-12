using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ApiCallers
{
    public interface IChesscomRepository
    {
        Task<IEnumerable<ChesscomGame>> GetAllChesscomAsync(string username);
    }
}