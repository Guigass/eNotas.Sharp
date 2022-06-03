using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Consulta
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("motivoStatus")]
        public object MotivoStatus { get; set; }

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

        [JsonProperty("forcarEmissaoContingencia", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ForcarEmissaoContingencia { get; set; }

        [JsonProperty("pedido", NullValueHandling = NullValueHandling.Ignore)]
        public Pedido Pedido { get; set; }

        [JsonProperty("cliente", NullValueHandling = NullValueHandling.Ignore)]
        public Cliente Cliente { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public long? Numero { get; set; }

        [JsonProperty("serie", NullValueHandling = NullValueHandling.Ignore)]
        public long? Serie { get; set; }

        [JsonProperty("dataEmissao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataEmissao { get; set; }

        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }

        [JsonProperty("transporte", NullValueHandling = NullValueHandling.Ignore)]
        public Transporte Transporte { get; set; }

        [JsonProperty("dataAutorizacao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataAutorizacao { get; set; }

        [JsonProperty("linkDanfe", NullValueHandling = NullValueHandling.Ignore)]
        public Uri LinkDanfe { get; set; }

        [JsonProperty("linkDownloadXml", NullValueHandling = NullValueHandling.Ignore)]
        public Uri LinkDownloadXml { get; set; }

        [JsonProperty("linkConsultaPorChaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkConsultaPorChaveAcesso { get; set; }

        [JsonProperty("protocolo", NullValueHandling = NullValueHandling.Ignore)]
        public Protocolo Protocolo { get; set; }

        [JsonProperty("emitidaEmContingencia", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EmitidaEmContingencia { get; set; }

        //[JsonProperty("itens", NullValueHandling = NullValueHandling.Ignore)]
        //public List<Iten> Itens { get; set; }

        [JsonProperty("valorTotal", NullValueHandling = NullValueHandling.Ignore)]
        public double? ValorTotal { get; set; }

        [JsonProperty("informacoesAdicionais", NullValueHandling = NullValueHandling.Ignore)]
        public string InformacoesAdicionais { get; set; }

        [JsonProperty("informacoesAdicionaisFisco")]
        public object InformacoesAdicionaisFisco { get; set; }

        [JsonProperty("webhookMetadados")]
        public object WebhookMetadados { get; set; }
    }
}
