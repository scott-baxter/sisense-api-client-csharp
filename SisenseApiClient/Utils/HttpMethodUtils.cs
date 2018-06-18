using System.Net.Http;

namespace SisenseApiClient.Utils
{
    public class HttpMethodUtils
    {
        public static HttpMethod Patch => new HttpMethod("PATCH");
    }
}
