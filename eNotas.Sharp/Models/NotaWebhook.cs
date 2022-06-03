using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class NotaWebhook
    {
        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("empresaId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmpresaId { get; set; }

        [JsonProperty("nfeId", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeId { get; set; }

        [JsonProperty("nfeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeStatus { get; set; }

        [JsonProperty("nfeMotivoStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeMotivoStatus { get; set; }

        [JsonProperty("nfeLinkDanfe", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeLinkDanfe { get; set; }

        [JsonProperty("nfeLinkXml", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeLinkXml { get; set; }

        [JsonProperty("nfeLinkConsultaPorChaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeLinkConsultaPorChaveAcesso { get; set; }

        [JsonProperty("conteudoQRCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ConteudoQrCode { get; set; }

        [JsonProperty("nfeNumero", NullValueHandling = NullValueHandling.Ignore)]
        public long? NfeNumero { get; set; }

        [JsonProperty("nfeSerie", NullValueHandling = NullValueHandling.Ignore)]
        public long? NfeSerie { get; set; }

        [JsonProperty("nfeChaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeChaveAcesso { get; set; }

        [JsonProperty("nfeDataEmissao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? NfeDataEmissao { get; set; }

        [JsonProperty("nfeDataAutorizacao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? NfeDataAutorizacao { get; set; }

        [JsonProperty("nfeNumeroProtocolo", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeNumeroProtocolo { get; set; }

        [JsonProperty("nfeDigestValue", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeDigestValue { get; set; }

        [JsonProperty("nfeWebHookMetadados", NullValueHandling = NullValueHandling.Ignore)]
        public string NfeWebHookMetadados { get; set; }
    }
}
