using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using DTOs.RealtorDTOs;
using System.IdentityModel.Tokens.Jwt;
using Helpers;

namespace Services
{
    public class RealtorAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationService _authenticationService;

        private readonly NotifyAuthState _notifyAuthState;

        public RealtorAuthenticationStateProvider(AuthenticationService authenticationService, NotifyAuthState notifyAuthState)
        {
            _authenticationService = authenticationService;
            _authenticationService.LoginChange += OnLoginChange;
            _notifyAuthState = notifyAuthState;
        }

        private void OnLoginChange(string? userName)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await _authenticationService.GetJwtAsync();

            if (string.IsNullOrEmpty(jwt))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, "jwt");

            // Extract roles from the token claims
            var roleClaims = token.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            foreach (var roleClaim in roleClaims)
            {
                identity.AddClaim(roleClaim);
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task MarkUserAsAuthenticated(RealtorLoginDTO userLogin)
        {
            await _authenticationService.LoginAsync(userLogin);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _authenticationService.LogoutAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
