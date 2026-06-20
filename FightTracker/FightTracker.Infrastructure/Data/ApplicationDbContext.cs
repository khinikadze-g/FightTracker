using FightTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<Fighter> Fighters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Fights)
                .HasForeignKey(f => f.EventId);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.FighterA)
                .WithMany(f => f.FightsAsFighterA)
                .HasForeignKey(f => f.FighterAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fight>()
                .HasOne(f => f.FighterB)
                .WithMany(f => f.FightsAsFighterB)
                .HasForeignKey(f => f.FighterBId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
