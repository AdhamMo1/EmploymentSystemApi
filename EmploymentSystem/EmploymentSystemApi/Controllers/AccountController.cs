using EmploymentSystemApi.Controllers.Base;
using EmploymentSystemApplication.DTOs.Authentication;
using EmploymentSystemApplication.Features.Authentication.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystemApi.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator) { }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new RegisterCommand(registerDto));

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.ExpirationOn);

            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new LoginCommand(loginDto));

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.ExpirationOn);

            return StatusCode(result.StatusCode, result);
        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            SetRefreshTokenInCookie(result.RefreshToken, result.ExpirationOn);

            return StatusCode(result.StatusCode, result);
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
