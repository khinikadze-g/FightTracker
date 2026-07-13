using FightTracker.Application.UserServices.cs;
using FightTracker.Core.Entities;
using FightTracker.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Users
{
    public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<Guid>;



    public class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var hash = passwordHasher.HashPassword(request.Password);
            var user = User.Create(request.FirstName, request.LastName, request.Email, hash);
            await userRepository.AddUserAsync(user);
            return user.Id;
        }
    }
}
