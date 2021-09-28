using BuscarCep.CrossCutting.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BuscarCep.Domain.WebRequest
{
    public class Response
    {
        public Response(HttpResponseMessage responseMessage)
        {
            BuildResponse(responseMessage);
        }

        public HttpStatusCode StatusCode { get; set; }

        public HttpResponseHeaders Headers { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        private string Content { get; set; }

        private string ErrorData { get; set; }

        public TData DeserializeSuccessData<TData>() where TData : class
        {
            if (!IsSuccessStatusCode)
                return null;

            if (!Content.TryDeserializeJson(out TData deserializedJson))
                throw new Exception($"Failed to deserialize response to {nameof(TData)}");

            return deserializedJson;
        }

        public object GetSuccessData()
        {
            return Content;
        }

        public TData DeserializeErrorData<TData>() where TData : class
        {
            if (IsSuccessStatusCode)
                return null;

            if (!ErrorData.TryDeserializeJson(out TData deserializedJson))
                Log.Warning("Failed to deserialize error data.");

            return deserializedJson;
        }

        public string GetError()
        {
            if (IsSuccessStatusCode)
                return null;

            return ErrorData;
        }

        public int GetResponseSize()
        {
            string _content =
                IsSuccessStatusCode
                ? Content
                : ErrorData;

            if (_content.IsNullOrEmpty())
                return 0;

            return Encoding.Unicode.GetByteCount(_content);
        }

        public T GetHeaderValue<T>(string headerName)
        {
            if (Headers.TryGetValues(headerName, out IEnumerable<string> values))
            {
                string session = values.FirstOrDefault();

                try
                {
                    return (T)Convert.ChangeType(session, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default;
                }
            }

            return default;
        }

        private void BuildResponse(HttpResponseMessage responseMessage)
        {
            StatusCode = responseMessage.StatusCode;

            IsSuccessStatusCode = responseMessage.IsSuccessStatusCode;

            Headers = responseMessage.Headers;

            string content = GetContent(responseMessage);

            if (IsSuccessStatusCode)
                Content = content;
            else
                ErrorData = content;
        }

        private static string GetContent(HttpResponseMessage responseMessage)
        {
            string content = responseMessage.Content.ReadAsStringAsync().Result;

            return content.Replace("\uFEFF", "");
        }
    }
}
