using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Contracts.DTOs
{
    public class AddFightDto
    {
        public int EventId { get; set; }
        public int FighterAId { get; set; }
        public int FighterBId { get; set; }

    }
}
