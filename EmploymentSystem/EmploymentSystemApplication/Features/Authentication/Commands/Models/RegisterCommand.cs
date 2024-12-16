using EmploymentSystemApplication.DTOs.Authentication;
using MediatR;

namespace EmploymentSystemApplication.Features.Authentication.Commands.Models
{
    public class RegisterCommand : IRequest<AuthenticationResponse>
    {
        public RegisterDto registerDto { get; set; }

        public RegisterCommand(RegisterDto registerDto)
        {
            this.registerDto = registerDto;
        }
    }
}
