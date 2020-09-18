using DataComp.Training.Api.Requriments;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataComp.Training.Api.AuthorizationHandlers
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(ClaimTypes.DateOfBirth));

            int years = DateTime.Now.Year - dateOfBirth.Year;

            if (years > requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;


        }
    }
}
