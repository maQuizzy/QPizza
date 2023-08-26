using Mapster;
using QPizza.Application.Authentication.Commands;
using QPizza.Application.Authentication.Common;
using QPizza.Application.Authentication.Queries;
using QPizza.Contracts.Authentication;

namespace QPizza.API.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
