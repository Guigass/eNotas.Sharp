using Newtonsoft.Json;
using System;

namespace eNotas.Sharp.Models
{
    public partial class ConsultaNfse
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("idExterno", NullValueHandling = NullValueHandling.Ignore)]
        public string IdExterno { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("motivoStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MotivoStatus { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("enviadaPorEmail", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnviadaPorEmail { get; set; }

        [JsonProperty("dataCriacao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataCriacao { get; set; }

        [JsonProperty("dataUltimaAlteracao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataUltimaAlteracao { get; set; }

        [JsonProperty("cliente", NullValueHandling = NullValueHandling.Ignore)]
        public Cliente Cliente { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public string Numero { get; set; }

        [JsonProperty("codigoVerificacao", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoVerificacao { get; set; }

        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }

        [JsonProperty("dataAutorizacao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataAutorizacao { get; set; }

        [JsonProperty("dataCancelamento", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataCancelamento { get; set; }

        [JsonProperty("linkDownloadPDF", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkDownloadPdf { get; set; }

        [JsonProperty("linkDownloadXML", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkDownloadXml { get; set; }

        [JsonProperty("numeroRps", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumeroRps { get; set; }

        [JsonProperty("serieRps", NullValueHandling = NullValueHandling.Ignore)]
        public string SerieRps { get; set; }

        [JsonProperty("dataCompetenciaRps", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataCompetenciaRps { get; set; }

        [JsonProperty("rpsGerenciado", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RpsGerenciado { get; set; }

        [JsonProperty("servico", NullValueHandling = NullValueHandling.Ignore)]
        public Servico Servico { get; set; }

        [JsonProperty("naturezaOperacao", NullValueHandling = NullValueHandling.Ignore)]
        public string NaturezaOperacao { get; set; }

        [JsonProperty("valorCofins", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorCofins { get; set; }

        [JsonProperty("valorCsll", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorCsll { get; set; }

        [JsonProperty("valorInss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorInss { get; set; }

        [JsonProperty("valorIr", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorIr { get; set; }

        [JsonProperty("valorPis", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorPis { get; set; }

        [JsonProperty("valorIss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorIss { get; set; }

        [JsonProperty("deducoes", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deducoes { get; set; }

        [JsonProperty("descontos", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Descontos { get; set; }

        [JsonProperty("descontoCondicionado", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DescontoCondicionado { get; set; }

        [JsonProperty("valorTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorTotal { get; set; }

        [JsonProperty("observacoes", NullValueHandling = NullValueHandling.Ignore)]
        public string Observacoes { get; set; }
    }
}
