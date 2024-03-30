using BeyondComputersNi.Blazor.Interfaces;
using System.Net.Http.Headers;

namespace BeyondComputersNi.Blazor.Authentication;

public class AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authToken = await authenticationService.GetAuthTokenAsync();
        var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(configuration["Api:Url"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(authToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
