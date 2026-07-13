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
    public class FighterRepository : IFighterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FighterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Fighter> AddFighterAsync(Fighter fighter)
        {
            await dbContext.AddAsync(fighter);
            await dbContext.SaveChangesAsync();
            return fighter;
        }

        public async Task<List<Fighter>> GetAllAsync()
        {
            return await dbContext.Fighters.ToListAsync();
        }

        public async Task<Fighter?> GetByIdAsync(int Id)
        {
            return await dbContext.Fighters.FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task<Fighter?> RemoveFighterAsync(int Id)
        {
            var existingFighter = await dbContext.Fighters.FirstOrDefaultAsync(f => f.Id == Id);
            if (existingFighter == null)
            {
                return null;
            }
            dbContext.Fighters.Remove(existingFighter);
            await dbContext.SaveChangesAsync();
            return existingFighter;
        }

        public async Task<Fighter?> UpdateAsync(int Id, Fighter fighter)
        {
            var existingFighter = dbContext.Fighters.FirstOrDefault(f => f.Id == Id);
            if (existingFighter == null)
            {
                return null;
            }
            existingFighter.FullName = fighter.FullName;
            existingFighter.NickName = fighter.NickName;
            existingFighter.WeightClass = fighter.WeightClass;
            existingFighter.Country = fighter.Country;
            existingFighter.Wins = fighter.Wins;
            existingFighter.Losses = fighter.Losses;
            existingFighter.Draws = fighter.Draws;
            await dbContext.SaveChangesAsync();
            return existingFighter;

        }
    }
}
