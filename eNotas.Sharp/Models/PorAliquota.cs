using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class PorAliquota
    {
        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }
    }
}
