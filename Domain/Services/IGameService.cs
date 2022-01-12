using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetAllAsync(string chesscomusr, string lichessusr);
        public Task<IEnumerable<Game>> GetAllChesscomAsync(string username);
        public Task<IEnumerable<Game>> GetAllLichessAsync(string username);
    }
}
