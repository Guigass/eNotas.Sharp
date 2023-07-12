using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class PorAliquota
    {
        [JsonProperty("baseCalculo", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BaseCalculo { get; set; }
        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }
        [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Valor { get; set; }
    }
}
