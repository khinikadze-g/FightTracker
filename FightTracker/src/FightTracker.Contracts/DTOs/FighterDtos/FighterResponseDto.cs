using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Contracts.DTOs.FighterDtos
{
    public class FighterResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? NickName { get; set; }
        public string WeightClass { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

    }
}
