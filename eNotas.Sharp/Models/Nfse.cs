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

        [JsonProperty("enviarPorEmail", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnviarPorEmail { get; set; }

        [Obsolete("Use EnviarPorEmail. Request JSON: enviarPorEmail (KB 170286).")]
        [JsonIgnore]
        public bool? EnviadaPorEmail
        {
            get => EnviarPorEmail;
            set => EnviarPorEmail = value;
        }

        [JsonProperty("cliente", NullValueHandling = NullValueHandling.Ignore)]
        public Cliente Cliente { get; set; }

        [JsonProperty("servico", NullValueHandling = NullValueHandling.Ignore)]
        public Servico Servico { get; set; }

        [JsonProperty("valorTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorTotal { get; set; }
    }
}
