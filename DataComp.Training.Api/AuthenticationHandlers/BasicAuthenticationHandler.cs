using DataComp.Training.IServices;
using DataComp.Training.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DataComp.Training.Api.AuthenticationHandlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string authorizationKey = "Authorization";

        private readonly IAuthenticateService authenticateService;

        public BasicAuthenticationHandler(
            IAuthenticateService authenticateService,
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
            this.authenticateService = authenticateService;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[authorizationKey]);

            if (authHeader.Scheme != "Basic")
            {
                return AuthenticateResult.Fail("Not Basic");
            }

            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            string username = credentials[0];
            string hashedPassword = credentials[1];
 
            if (!authenticateService.TryAuthenticate(username, hashedPassword, out User user))
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);

            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim("Stanowisko", "ST1"));
            identity.AddClaim(new Claim("Stanowisko", "ST2"));
            identity.AddClaim(new Claim("Stanowisko", "ST3"));

            identity.AddClaim(new Claim("User", user.ToString()));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
