using DataComp.Training.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace DataComp.Training.Api.Services
{
    public class ClaimService : IClaimService
    {
        public IEnumerable<Claim> Get(User user)
        {
            IEnumerable<Claim> claims = new Collection<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Stanowisko", "ST1"),
                new Claim("Stanowisko", "ST2"),
                new Claim("Stanowisko", "ST3"),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString("yyyy-MM-dd")),
                new Claim(ClaimTypes.Role, "Trainer"), 
            };

            return claims;
        }
    }
}
