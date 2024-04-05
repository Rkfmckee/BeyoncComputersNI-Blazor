using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.Pages.Authentication.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;

namespace BeyondComputersNi.Blazor.Authentication;

public class AuthenticationHandler(ITokenService tokenService, IConfiguration configuration,
    AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager) : DelegatingHandler
{
    private bool refreshing;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await AppendAuthTokenToRequest(request);

        var response = await base.SendAsync(request, cancellationToken);

        if (!refreshing && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                refreshing = true;

                await authenticationStateProvider.GetAuthenticationStateAsync();

                await AppendAuthTokenToRequest(request);

                response = await base.SendAsync(request, cancellationToken);
            }
            finally
            { 
                refreshing = false;
            }
        }

        return response;
    }

    private async Task AppendAuthTokenToRequest(HttpRequestMessage request)
    {
        var authToken = await tokenService.GetAuthTokenAsync();
        var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(configuration["Api:Url"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(authToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }
}
