using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Protocolo
    {
        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public string Numero { get; set; }

        [JsonProperty("digestValue", NullValueHandling = NullValueHandling.Ignore)]
        public string DigestValue { get; set; }
    }
}
