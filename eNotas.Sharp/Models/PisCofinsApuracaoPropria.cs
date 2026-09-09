using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class PisCofinsApuracaoPropria
    {
        [JsonProperty("baseCalculo", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BaseCalculo { get; set; }

        [JsonProperty("aliquotaPis", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaPis { get; set; }

        [JsonProperty("valorPis", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorPis { get; set; }

        [JsonProperty("aliquotaCofins", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaCofins { get; set; }

        [JsonProperty("valorCofins", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorCofins { get; set; }
    }
}
