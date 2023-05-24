using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace IntegrationTests.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TResponse> PostAsJsonReadResponseAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest value, CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync(requestUri, value, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }

    public static async Task<TResponse> PutAsJsonReadResponseAsync<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest value, CancellationToken cancellationToken = default)
    {
        var response = await client.PutAsJsonAsync(requestUri, value, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonConvert.DeserializeObject<TResponse>(responseString);
    }
}