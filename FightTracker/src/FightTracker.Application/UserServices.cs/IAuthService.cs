using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.UserServices.cs
{
    public interface IAuthService
    {
        Task<string> ValidateUserAsync(string email, string password);
    }
}
