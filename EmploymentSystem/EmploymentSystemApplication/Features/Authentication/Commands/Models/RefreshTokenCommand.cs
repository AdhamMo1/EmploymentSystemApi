using EmploymentSystemApplication.DTOs.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<AuthenticationResponse>
    {
        public string token {  get; set; }

        public RefreshTokenCommand(string token)
        {
            this.token = token;
        }
    }
}
