using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Imposto
    {
        [JsonProperty("situacaoTributaria", NullValueHandling = NullValueHandling.Ignore)]
        public string SituacaoTributaria { get; set; }

        [JsonProperty("porAliquota", NullValueHandling = NullValueHandling.Ignore)]
        public PorAliquota PorAliquota { get; set; }
    }
}
