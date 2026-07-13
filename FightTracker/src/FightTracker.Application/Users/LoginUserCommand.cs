using FightTracker.Application.UserServices.cs;
using FightTracker.Contracts.DTOs.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Users
{
    public record LoginUserCommand(string Email, string Password) : IRequest<AuthUserDto?>;



    public class LoginUserCommandHandler(IAuthService authService) : IRequestHandler<LoginUserCommand, AuthUserDto?>
    {
        public async Task<AuthUserDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = await authService.ValidateUserAsync(request.Email, request.Password);
            return new AuthUserDto(token);
        }
    }
}
