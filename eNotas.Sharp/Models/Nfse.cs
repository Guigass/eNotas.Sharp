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

        [JsonProperty("numeroRps", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumeroRps { get; set; }

        [JsonProperty("serieRps", NullValueHandling = NullValueHandling.Ignore)]
        public string SerieRps { get; set; }

        [JsonProperty("dataCompetencia", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataCompetencia { get; set; }

        [JsonProperty("naturezaOperacao", NullValueHandling = NullValueHandling.Ignore)]
        public string NaturezaOperacao { get; set; }

        [JsonProperty("observacoes", NullValueHandling = NullValueHandling.Ignore)]
        public string Observacoes { get; set; }

        [JsonProperty("dadosAdicionaisEmail", NullValueHandling = NullValueHandling.Ignore)]
        public DadosAdicionaisEmail DadosAdicionaisEmail { get; set; }

        [JsonProperty("deducoes", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deducoes { get; set; }

        [JsonProperty("descontos", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Descontos { get; set; }

        [JsonProperty("descontoCondicionado", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DescontoCondicionado { get; set; }
    }
}
