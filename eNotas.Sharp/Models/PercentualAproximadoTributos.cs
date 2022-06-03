using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class PercentualAproximadoTributos
    {
        [JsonProperty("simplificado", NullValueHandling = NullValueHandling.Ignore)]
        public Simplificado Simplificado { get; set; }

        [JsonProperty("detalhado", NullValueHandling = NullValueHandling.Ignore)]
        public Detalhado Detalhado { get; set; }

        [JsonProperty("fonte", NullValueHandling = NullValueHandling.Ignore)]
        public string Fonte { get; set; }
    }
}
