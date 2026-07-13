using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Contracts.DTOs.FightDtos
{
    public class UpdateFightResultDto
    {
        public int? WinnerId { get; set; }
        public string? Method { get; set; } = null!;
        public int? Round { get; set; }
        public string? Time { get; set; }
    }
}
