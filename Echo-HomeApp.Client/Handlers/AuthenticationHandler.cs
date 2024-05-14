using Services;
using System.Net.Http.Headers;

namespace Handlers
{
    /// <summary>
    /// Author: Samed Salman
    /// This class handles adding the JWT to API-requests made by the httpClient
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retrieve cached jwt
            var jwt = await _authenticationService.GetJwtAsync();
            // 
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["applicationUrl"] ?? "") ?? false;
            if (isToServer && !string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
