using System;
using System.Net;
using System.Net.Http;

namespace SisenseApiClient.Exceptions
{
    public class SisenseClientHttpException : HttpRequestException
    {
        public HttpStatusCode SatusCode { get; set; }

        public HttpRequestMessage RequestMessage { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }

        public string ResponseContent { get; set; }

        public SisenseClientHttpException()
        {
        }

        public SisenseClientHttpException(string message) : base(message)
        {
        }

        public SisenseClientHttpException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
