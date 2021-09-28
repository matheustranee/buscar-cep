using Newtonsoft.Json;

namespace BuscarCep.Domain.DTO
{
    public class CepDTO
    {
        public CepDTO()
        {
        }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Rua { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        
        [JsonProperty("localidade")]
        public string Cidade { get; set; }
        
        [JsonProperty("uf")]
        public string Estado { get; set; }
        
        [JsonProperty("ibge")]
        public string Ibge { get; set; }
        
        [JsonProperty("gia")]
        public string Gia { get; set; }
        
        [JsonProperty("ddd")]
        public string Ddd { get; set; }
        
        [JsonProperty("siafi")]
        public string Siafi { get; set; }
    }
}
