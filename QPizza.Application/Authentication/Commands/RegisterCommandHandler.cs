using ErrorOr;
using MediatR;
using QPizza.Application.Authentication.Common;
using QPizza.Application.Common.Interfaces.Authentication;
using QPizza.Application.Common.Interfaces.Persistence;
using System;
using QPizza.Domain.Common.Errors;
using QPizza.Domain.Entities;

namespace QPizza.Application.Authentication.Commands
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_userRepository.GetUserByEmail(request.Email) != null)
            {
                return Errors.User.DuplicateEmail;
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
