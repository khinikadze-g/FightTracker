using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Entities
{
    public class Fighter
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? NickName { get; set; }
        public string WeightClass { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }

        public List<Fight> FightsAsFighterA { get; set; } = new ();
        public List<Fight> FightsAsFighterB { get; set; } = new();


    }
}
