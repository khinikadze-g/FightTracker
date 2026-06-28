using FightTracker.Application.UserServices.cs;
using FightTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Infrastructure.Auth
{
    public class AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator
        ) : IAuthService
    {
        public async Task<string> ValidateUserAsync(string email, string password)
        {
            var user = await userRepository.FindByEmailAsync(email);
            if ( user == null  || !passwordHasher.ValidateHash(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }
            return tokenGenerator.GenerateToken(user);
        }
    }
}
