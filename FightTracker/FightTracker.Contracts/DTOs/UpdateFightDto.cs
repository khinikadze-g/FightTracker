using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Contracts.DTOs
{
    public class UpdateFightDto
    {
        public int FighterAId { get; set; }
        public int FighterBId { get; set; }
    }
}
