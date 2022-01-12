using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ApiCallers
{
    public interface ILichessRepository
    {
        public Task<IEnumerable<LichessGame>> GetAllLichessAsync(string username);
    }
}
