using AutoMapper;
using EmploymentSystemApplication.DTOs.Authentication;
using EmploymentSystemApplication.ServicesContract;
using EmploymentSystemDomain.Entities;
using EmploymentSystemDomain.Entities.SecurityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmploymentSystemApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        public AuthenticationService(UserManager<ApplicationUser> userManager,IOptions<JWT> jwt, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwt = jwt.Value;
        }

        public async Task<AuthenticationResponse> LoginServiceAsync(LoginDto loginDto)
        {
            var Response = new AuthenticationResponse();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new AuthenticationResponse { Message = "Invalid Email or Password!", StatusCode = StatusCodes.Status400BadRequest };

            var jwtSecurityToken = await CreateJwtToken(user);

            Response.Email = user.Email;
            Response.Username = user.UserName;
            Response.IsAuthenticated = true;
            Response.StatusCode = StatusCodes.Status200OK;
            Response.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            Response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            if (user.RefreshTokens.Any(prop => prop.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                Response.RefreshToken = activeRefreshToken.Token;
                Response.ExpirationOn = activeRefreshToken.ExpireOn;
            }
            else
            {
                var NewRefreshToken = GenerateRefreshToken();
                Response.RefreshToken = NewRefreshToken.Token;
                Response.ExpirationOn = NewRefreshToken.ExpireOn;
                user.RefreshTokens.Add(NewRefreshToken);
                await _userManager.UpdateAsync(user);
            }
            return Response;
        }

        public async Task<AuthenticationResponse> RegisterServiceAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
                return new AuthenticationResponse { Message = "Email is already registered, try another!", StatusCode = StatusCodes.Status400BadRequest };

            if (await _userManager.FindByNameAsync(registerDto.UserName) is not null)
                return new AuthenticationResponse { Message = "UserName is already registered, try another!", StatusCode = StatusCodes.Status400BadRequest };

            var newUser = _mapper.Map<ApplicationUser>(registerDto);
            var result = await _userManager.CreateAsync(newUser,registerDto.Password);

            if(!result.Succeeded)
            {
                var errors = string.Empty;
                foreach(var error in result.Errors)
                {
                    errors += $"{error}, ";
                }
                return new AuthenticationResponse { Message = errors , StatusCode = StatusCodes.Status400BadRequest };
            }
            await _userManager.AddToRoleAsync(newUser, registerDto.Role.ToString());
            var jwtSecurityToken = await CreateJwtToken(newUser);

            var NewRefreshToken = GenerateRefreshToken();
            newUser.RefreshTokens.Add(NewRefreshToken);
            await _userManager.UpdateAsync(newUser);

            return new AuthenticationResponse
            {
                IsAuthenticated = true,
                StatusCode = StatusCodes.Status200OK,
                Email = newUser.Email,
                Username = newUser.UserName,
                Roles = (List<string>)await _userManager.GetRolesAsync(newUser),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshToken = NewRefreshToken.Token,
                ExpirationOn = NewRefreshToken.ExpireOn,
            };
        }
        public async Task<AuthenticationResponse> RefreshTokenAsync(string token)
        {
            var authModel = new AuthenticationResponse();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                authModel.Message = "Invalid token";
                authModel.StatusCode = StatusCodes.Status400BadRequest;
                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive token";
                authModel.StatusCode = StatusCodes.Status400BadRequest;
                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.ExpirationOn = newRefreshToken.ExpireOn;
            authModel.StatusCode = StatusCodes.Status200OK;

            return authModel;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
