using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Impostos
    {
        [JsonProperty("percentualAproximadoTributos", NullValueHandling = NullValueHandling.Ignore)]
        public PercentualAproximadoTributos PercentualAproximadoTributos { get; set; }

        [JsonProperty("icms", NullValueHandling = NullValueHandling.Ignore)]
        public Icms Icms { get; set; }

        [JsonProperty("pis", NullValueHandling = NullValueHandling.Ignore)]
        public Imposto Pis { get; set; }

        [JsonProperty("cofins", NullValueHandling = NullValueHandling.Ignore)]
        public Imposto Cofins { get; set; }

        [JsonProperty("ipi", NullValueHandling = NullValueHandling.Ignore)]
        public Imposto Ipi { get; set; }
    }
}
