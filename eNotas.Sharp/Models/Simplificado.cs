using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Simplificado
    {
        [JsonProperty("percentual", NullValueHandling = NullValueHandling.Ignore)]
        public long? Percentual { get; set; }
    }
}
