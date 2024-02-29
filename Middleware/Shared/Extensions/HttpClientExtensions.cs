using System.Diagnostics.CodeAnalysis;
using System.Text;
using Shared.Validation;

namespace Shared.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value) where T : class
        {
            return PostAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, [NotNull] Uri requestUri, T value, CancellationToken cancellationToken) where T : class
        {
            Assume.NotNull(client, nameof(client));
            Assume.NotNull(requestUri, nameof(requestUri));
            Assume.NotNull(value, nameof(value));

            var json = await value.AsJsonStringAsync(cancellationToken).ConfigureAwait(true);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.MediaTypeNames.Application.Json);

            return await client.PostAsync(requestUri, httpContent, cancellationToken).ConfigureAwait(true);
        }
    }
}