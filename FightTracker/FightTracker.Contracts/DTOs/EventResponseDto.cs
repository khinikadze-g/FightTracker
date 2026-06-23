using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Contracts.DTOs
{
    public class EventResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string eventStatus { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public List<int> FightIds  { get; set; } = new();
    }
}
