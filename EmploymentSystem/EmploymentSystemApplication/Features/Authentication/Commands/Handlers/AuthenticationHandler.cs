using EmploymentSystemApplication.DTOs.Authentication;
using EmploymentSystemApplication.Features.Authentication.Commands.Models;
using EmploymentSystemApplication.ServicesContract;
using MediatR;

namespace EmploymentSystemApplication.Features.Authentication.Commands.Handlers
{
    public class AuthenticationHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>,
                                         IRequestHandler<LoginCommand, AuthenticationResponse>,
                                         IRequestHandler<RefreshTokenCommand, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.RegisterServiceAsync(request.registerDto);   
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.LoginServiceAsync(request.loginDto);
        }

        public async Task<AuthenticationResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.RefreshTokenAsync(request.token);
        }
    }
}
