using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Services;
using LinuxLudo.API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LinuxLudo.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }


        public string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.Value.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Issuer,
                claims,
                expires : expires,
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}