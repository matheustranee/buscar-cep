using BuscarCep.Domain.Enum;
using BuscarCep.Domain.WebRequest;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BuscarCep.Domain.Builders
{
    public class RequestBuilder
    {
        protected string Url { get; set; }

        protected object Payload { get; set; }

        protected HttpMethod HttpMethod { get; set; }        

        protected Encoding Encoding { get; set; }

        protected EMediaType MediaType { get; set; }

        protected EClient Client { get; set; }

        protected EPayload PayloadType { get; set; }

        protected Dictionary<string, string> UrlEncodedKeyPairs { get; set; }

        protected Dictionary<string, string> HeadersKeyPairs { get; set; }

        public static RequestBuilder Create(EClient client, HttpMethod method, string url)
        {
            return new RequestBuilder
            {
                Client = client,
                HttpMethod = method,
                Url = url,
                MediaType = EMediaType.Json,
                Encoding = Encoding.UTF8,
                UrlEncodedKeyPairs = new Dictionary<string, string>(),
                HeadersKeyPairs = new Dictionary<string, string>(),
            };
        }

        public RequestBuilder WithPayload(object payload)
        {
            Payload = payload;

            return this;
        }
        
        public RequestBuilder WithPayloadType(EPayload payloadType)
        {
            PayloadType = payloadType;

            return this;
        }

        public RequestBuilder WithMediaType(EMediaType mediaType)
        {
            MediaType = mediaType;

            return this;
        }

        public RequestBuilder WithEncoding(Encoding encoding)
        {
            Encoding = encoding;

            return this;
        }

        public RequestBuilder WithUrlEncodedKeyPairs(Dictionary<string, string> urlEncodedKeyPairs)
        {
            MediaType = EMediaType.UrlEncoded;
            UrlEncodedKeyPairs = urlEncodedKeyPairs;

            return this;
        }

        public RequestBuilder WithHeaders(Dictionary<string, string> headersKeyPairs)
        {
            HeadersKeyPairs = 
                HeadersKeyPairs.Concat(headersKeyPairs)
                               .ToDictionary(keyValue => keyValue.Key, 
                                             keyValue => keyValue.Value);
            return this;
        }
        
        public RequestBuilder WithHeader(string key, string value)
        {
            HeadersKeyPairs.Add(key, value);

            return this;
        }

        public Request Build()
        {
            return new Request
            {
                HttpMethod = HttpMethod,
                Client = Client,
                Url = Url,
                Encoding = Encoding,
                MediaType = MediaType,
                Payload = Payload,
                PayloadType = PayloadType,
                UrlEncodedKeyPairs = UrlEncodedKeyPairs,
                HeadersKeyPairs = HeadersKeyPairs
            };
        }
    }
}
