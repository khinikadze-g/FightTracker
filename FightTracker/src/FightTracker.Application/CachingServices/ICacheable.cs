using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.CachingServices
{
    public interface ICacheable
    {
        public string Key { get; }
        public TimeSpan Expiration { get; }
    }
}
