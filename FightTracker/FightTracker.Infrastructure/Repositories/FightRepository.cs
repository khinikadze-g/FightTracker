using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using FightTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Repositories
{
    public class FightRepository : IFightRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FightRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Fight> AddFightAsync(Fight fight)
        {
            await dbContext.Fights.AddAsync(fight);
            return fight;
        }

        public async Task<Fight?> DeleteByIdAsync(int id)
        {
            var existingFight = await dbContext.Fights.FirstOrDefaultAsync(f => f.Id == id);
            if (existingFight == null)
            {
                return null;
            }
            dbContext.Fights.Remove(existingFight);
            await dbContext.SaveChangesAsync();
            return existingFight;
        }

        public async Task<List<Fight>> GetAllAsync()
        {
            return await dbContext.Fights.ToListAsync();
        }

        public async Task<List<Fight>> GetByIdsAsync(IEnumerable<int> Id)
        {
            return await dbContext.Fights.Where(f => Id.Contains(f.Id)).ToListAsync();
        }

        public async Task<Fight?> GetByIdAsync(int id)
        {
            return await dbContext.Fights.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Fight?> UpdateFightAsync(int id, Fight fight)
        {
            var existingFight = await dbContext.Fights.FirstOrDefaultAsync(f => f.Id == id);
            if (existingFight == null)
            {
                return null;
            }
            existingFight.FighterAId = fight.FighterAId;
            existingFight.FighterBId = fight.FighterBId;
            existingFight.WinnerId = fight.WinnerId;
            existingFight.Method = fight.Method;
            existingFight.Round = fight.Round;
            existingFight.Time = fight.Time;
           
            return existingFight;

            

        }
    }
}
