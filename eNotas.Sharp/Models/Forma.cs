using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Forma
    {
        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Valor { get; set; }

        [JsonProperty("descricao", NullValueHandling = NullValueHandling.Ignore)]
        public string Descricao { get; set; }

        [JsonProperty("credenciadoraCartao", NullValueHandling = NullValueHandling.Ignore)]
        public CredenciadoraCartao CredenciadoraCartao { get; set; }
    }
}
