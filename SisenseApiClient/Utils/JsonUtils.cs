using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SisenseApiClient.Utils
{
    internal class JsonUtils
    {
        public static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }            
        };

        public static JsonSerializerSettings DeserializerSettings => new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };
    }
}
