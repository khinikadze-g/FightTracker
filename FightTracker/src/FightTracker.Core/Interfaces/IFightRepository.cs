using FightTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Interfaces
{
    public interface IFightRepository
    {
        Task<List<Fight>> GetAllAsync();
        Task<List<Fight>> GetByIdsAsync(IEnumerable<int> id);
        Task<Fight?> GetByIdAsync(int id);
        Task<Fight> AddFightAsync(Fight fight);
        Task<Fight?> UpdateFightAsync(int id, Fight fight);
        Task<Fight?> DeleteByIdAsync(int id);
    }
}
