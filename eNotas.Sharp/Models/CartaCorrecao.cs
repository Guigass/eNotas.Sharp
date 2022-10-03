using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class CartaCorrecao
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public long? Numero { get; set; }

        [JsonProperty("correcao", NullValueHandling = NullValueHandling.Ignore)]
        public string Correcao { get; set; }

        [JsonProperty("nfe", NullValueHandling = NullValueHandling.Ignore)]
        public Nfe Nfe { get; set; }
    }

    public partial class Nfe
    {
        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }
    }
}
