using DataComp.Training.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace DataComp.Training.Api.Services
{
    public interface IClaimService
    {
        IEnumerable<Claim> Get(User user);
    }
}
