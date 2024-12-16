using EmploymentSystemApplication.DTOs.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Authentication.Commands.Models
{
    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public LoginDto loginDto { get; set; }

        public LoginCommand(LoginDto loginDto)
        {
            this.loginDto = loginDto;
        }
    }
}
