using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Transporte
    {
        [JsonProperty("frete", NullValueHandling = NullValueHandling.Ignore)]
        public Frete Frete { get; set; }

        [JsonProperty("enderecoEntrega", NullValueHandling = NullValueHandling.Ignore)]
        public Endereco EnderecoEntrega { get; set; }

        [JsonProperty("transportadora", NullValueHandling = NullValueHandling.Ignore)]
        public Transportadora Transportadora { get; set; }

        [JsonProperty("veiculo", NullValueHandling = NullValueHandling.Ignore)]
        public Veiculo Veiculo { get; set; }

        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public Volume Volume { get; set; }
    }

}
