using EmploymentSystemApplication.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.ServicesContract
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> RegisterServiceAsync(RegisterDto registerDto);
        Task<AuthenticationResponse> LoginServiceAsync(LoginDto loginDto);
        Task<AuthenticationResponse> RefreshTokenAsync(string token);

    }
}
