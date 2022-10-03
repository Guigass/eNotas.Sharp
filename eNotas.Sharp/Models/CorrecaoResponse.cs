using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class CorrecaoResponse
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("motivoStatus")]
        public object MotivoStatus { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public long? Numero { get; set; }

        [JsonProperty("correcao", NullValueHandling = NullValueHandling.Ignore)]
        public string Correcao { get; set; }

        [JsonProperty("condicoesUso", NullValueHandling = NullValueHandling.Ignore)]
        public string CondicoesUso { get; set; }

        [JsonProperty("nfe", NullValueHandling = NullValueHandling.Ignore)]
        public Nfe Nfe { get; set; }

        [JsonProperty("protocoloAutorizacao", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtocoloAutorizacao { get; set; }

        [JsonProperty("dataCriacao", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DataCriacao { get; set; }

        [JsonProperty("dataUltimaAlteracao", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DataUltimaAlteracao { get; set; }
    }
}
