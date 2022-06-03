using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Frete
    {
        [JsonProperty("modalidade", NullValueHandling = NullValueHandling.Ignore)]
        public string Modalidade { get; set; }

        [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Valor { get; set; }
    }
}
