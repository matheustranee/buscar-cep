using BuscarCep.CrossCutting.Extensions;
using BuscarCep.Domain.Enum;
using BuscarCep.Domain.Interfaces.Facades;
using BuscarCep.Domain.WebRequest;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuscarCep.CrossCutting.WebRequest
{
    public class RequestFacade : IRequestFacade
    {
        private IHttpClientFactory _clientFactory;

        public RequestFacade(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Response> CreateRequestAsync(Request request)
        {
            HttpClient _client = GetClient(request);

            Stopwatch stopwatch = Stopwatch.StartNew();

            Response response = request.HttpMethod.Method switch
            {
                "GET" => await GetAsync(request, _client),
                "POST" => await PostAsync(request, _client),
                "PUT" => await PutAsync(request, _client),
                "HEAD" => await HeadAsync(request, _client),
                _ => throw new ArgumentException($"HttpMethod {request.HttpMethod.Method} not suported."),
            };

            stopwatch.Stop();            

            return response;
        }        

        private HttpClient GetClient(Request request)
        {
            HttpClient _client = _clientFactory.CreateClient(request.Client.GetDescription());

            _client.DefaultRequestHeaders.Accept.Clear();            

            if (request.HeadersKeyPairs.Any())
                foreach (var header in request.HeadersKeyPairs)
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);

            return _client;
        }

        private async Task<Response> GetAsync(Request request, HttpClient client)
        {
            HttpResponseMessage _responseMessage =
                await client.GetAsync(request.Url);

            return new Response(_responseMessage);
        }

        private async Task<Response> PostAsync(Request request, HttpClient client)
        {
            HttpResponseMessage _responseMessage =
                await client.PostAsync(request.Url, GenerateContent(request));

            return new Response(_responseMessage);
        }

        private async Task<Response> PutAsync(Request request, HttpClient client)
        {
            HttpResponseMessage _responseMessage =
                await client.PutAsync(request.Url, GenerateContent(request));

            return new Response(_responseMessage);
        }

        private async Task<Response> HeadAsync(Request request, HttpClient client)
        {
            HttpResponseMessage _responseMessage =
                await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, request.Url));

            return new Response(_responseMessage);
        }

        private string GetRequestPayload(Request request)
        {
            if (request.PayloadType == EPayload.Json)
                return request.Payload.ToJson();

            if (request.Payload is string)
                return request.Payload as string;

            throw new InvalidCastException("Cannot cast the current payload.");
        }

        private HttpContent GenerateContent(Request request)
        {
            HttpContent _content =
                request.MediaType == EMediaType.UrlEncoded
                    ? new FormUrlEncodedContent(request.UrlEncodedKeyPairs)
                    : new StringContent(GetRequestPayload(request), request.Encoding, request.MediaType.GetDescription());

            if (request.Encoding == null)
                _content.Headers.ContentType.CharSet = "";

            return _content;
        }
    }
}
