using DataComp.Training.IServices;
using DataComp.Training.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Services
{


    public class JwtTokenService : ITokenService
    {
        private readonly IClaimService claimService;

        public JwtTokenService(IClaimService claimService)
        {
            this.claimService = claimService;
        }

        public string CreateToken(User user)
        {
            string secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity = new ClaimsIdentity();

            identity.AddClaims(claimService.Get(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
            };

            SecurityToken securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
