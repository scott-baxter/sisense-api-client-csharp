using System.Collections.Generic;
using System.Text;

namespace SisenseApiClient.Utils
{
    internal class QueryStringBuilder
    {
        private List<KeyValuePair<string, string>> _queryParameters = new List<KeyValuePair<string, string>>();

        public QueryStringBuilder AddParameter(string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _queryParameters.Add(new KeyValuePair<string, string>(name, value));
            }

            return this;
        }

        public string Build()
        {
            var stringBuilder = new StringBuilder("?");

            foreach (var queryParameter in _queryParameters)
            {
                if (!string.IsNullOrEmpty(queryParameter.Value))
                {
                    stringBuilder.Append(queryParameter.Key.ToLower() + "=" + queryParameter.Value + "&");
                }
            }

            string queryString = stringBuilder.ToString().TrimEnd('&');

            if (queryString == "?")
            {
                return string.Empty;
            }

            return queryString;
        }
    }
}
