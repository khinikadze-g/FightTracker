using FightTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.UserServices.cs
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
