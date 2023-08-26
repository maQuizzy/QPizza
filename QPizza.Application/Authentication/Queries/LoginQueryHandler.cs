using ErrorOr;
using MediatR;
using QPizza.Application.Authentication.Common;
using QPizza.Application.Common.Interfaces.Authentication;
using QPizza.Application.Common.Interfaces.Persistence;
using QPizza.Domain.Entities;
using QPizza.Domain.Common.Errors;

namespace QPizza.Application.Authentication.Queries
{
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_userRepository.GetUserByEmail(request.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (request.Password != user.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
