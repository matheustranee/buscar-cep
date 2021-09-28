using BuscarCep.Domain.Enum;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BuscarCep.Domain.WebRequest
{
    public class Request
    {      
        public string Url { get; set; }             

        public object Payload { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Encoding Encoding { get; set; }

        public EMediaType MediaType { get; set; }

        public EClient Client { get; set; }

        public EPayload PayloadType { get; set; }

        public Dictionary<string, string> UrlEncodedKeyPairs { get; set; }

        public Dictionary<string, string> HeadersKeyPairs { get; set; }
    }
}
