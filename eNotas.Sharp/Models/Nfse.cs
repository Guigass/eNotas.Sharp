using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Nfse
    {
        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("idExterno", NullValueHandling = NullValueHandling.Ignore)]
        public string IdExterno { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("enviadaPorEmail", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnviadaPorEmail { get; set; }

        [JsonProperty("cliente", NullValueHandling = NullValueHandling.Ignore)]
        public Cliente Cliente { get; set; }

        [JsonProperty("servico", NullValueHandling = NullValueHandling.Ignore)]
        public Servico Servico { get; set; }

        [JsonProperty("valorTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorTotal { get; set; }
    }
}
