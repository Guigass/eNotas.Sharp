using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class ConsultaInutilizacao
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("motivoStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MotivoStatus { get; set; }

        [JsonProperty("serie", NullValueHandling = NullValueHandling.Ignore)]
        public int Serie { get; set; }

        [JsonProperty("numeroInicial", NullValueHandling = NullValueHandling.Ignore)]
        public int NumeroInicial { get; set; }

        [JsonProperty("numeroFinal", NullValueHandling = NullValueHandling.Ignore)]
        public int NumeroFinal { get; set; }

        [JsonProperty("justificativa", NullValueHandling = NullValueHandling.Ignore)]
        public string Justificativa { get; set; }

        [JsonProperty("protocoloAutorizacao")]
        public object ProtocoloAutorizacao { get; set; }

        [JsonProperty("dataCriacao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataCriacao { get; set; }

        [JsonProperty("dataUltimaAlteracao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataUltimaAlteracao { get; set; }
    }
}
