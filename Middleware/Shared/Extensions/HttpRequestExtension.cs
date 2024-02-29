using Microsoft.AspNetCore.Http;

namespace Shared.Extensions
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// RequestUri
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Uri</returns>
        public static Uri RequestUri(this HttpRequest req)
        {
            var uri = new UriBuilder
            {
                Scheme = req.Scheme,
                Host = req.Host.Host,
                Port = req.Host.Port ?? 80,
                Path = req.PathBase.Add(req.Path),
                Query = req.QueryString.ToString()
            }.Uri;

            return uri;
        }

        /// <summary>
        /// Content
        /// </summary>
        /// <param name="req"></param>
        /// <returns>StreamContent</returns>
        public static StreamContent Content(this HttpRequest req)
        {
            return new StreamContent(req.Body);
        }
    }
}
