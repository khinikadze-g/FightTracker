using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Entities
{
    public class Fight
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int FighterAId { get; set; }
        public Fighter FighterA { get; set; } = null!;

        public int FighterBId { get;set; }
        public Fighter FighterB { get; set; } = null!;
        
        public int WinnerId { get; set; }
        public Fighter Winner { get; set;} = null!;

        public string Method { get; set; } = null!;
        public int? Round { get; set; }
        public string? Time { get; set; }

    }
}
