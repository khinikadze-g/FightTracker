using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.UserServices.cs
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool ValidateHash(string password, string hash);
    }
}
