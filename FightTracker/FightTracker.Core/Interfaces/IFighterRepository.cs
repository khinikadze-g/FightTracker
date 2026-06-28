using FightTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Interfaces
{
    public interface IFighterRepository
    {
        Task<Fighter> AddFighterAsync(Fighter fighter);
        Task<List<Fighter>> GetAllAsync();
        Task<Fighter?> GetByIdAsync(int Id);
        Task<Fighter?> UpdateAsync(int Id, Fighter fighter);
        Task<Fighter?> RemoveFighterAsync(int Id);
    }
}
