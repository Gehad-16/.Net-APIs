using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration  configuration )
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser User )
        {
            var AuthClaim = new List<Claim>()
            {
                new Claim( ClaimTypes.GivenName , User.DisplayName ),
                new Claim( ClaimTypes.Email , User.Email )
            };
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
           
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationDays"])),
                claims: AuthClaim,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
            
        }
    }
}
