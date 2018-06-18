using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SisenseApiClient.Utils
{
    internal class JsonUtils
    {
        public static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }            
        };
    }
}
