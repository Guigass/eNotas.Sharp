using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Detalhado
    {
        [JsonProperty("percentualFederal", NullValueHandling = NullValueHandling.Ignore)]
        public long? PercentualFederal { get; set; }

        [JsonProperty("percentualEstadual", NullValueHandling = NullValueHandling.Ignore)]
        public long? PercentualEstadual { get; set; }

        [JsonProperty("percentualMunicipal", NullValueHandling = NullValueHandling.Ignore)]
        public long? PercentualMunicipal { get; set; }
    }
}
