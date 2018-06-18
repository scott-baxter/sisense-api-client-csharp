using Newtonsoft.Json;

namespace SisenseApiClient.Services.Authentication.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        // TODO: profile format?
        //public object Profile { get; set; }
    }
}
