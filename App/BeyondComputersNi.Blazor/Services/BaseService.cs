using System.Net.Http.Json;

namespace BeyondComputersNi.Blazor.Services;

public class BaseService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private HttpClient httpClient => httpClientFactory.CreateClient(configuration["Api:HttpClient"] ?? "");

    protected async Task<T> PostAsync<T>(string url, object requestBody)
    {
        var response = await httpClient.PostAsync(url, JsonContent.Create(requestBody));
        if (!response.IsSuccessStatusCode)
        {
            var responseBodyMessage = await response.Content.ReadAsStringAsync();
            var errorMessage = string.IsNullOrEmpty(responseBodyMessage) ? $"{response.StatusCode}" : $"{response.StatusCode}: {responseBodyMessage}";

            throw new Exception(errorMessage);
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        if (content is null) throw new InvalidDataException();

        return content;
    }
}
