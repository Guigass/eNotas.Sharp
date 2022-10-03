using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Nfe
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore))]
        public object Id { get; set; }

        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }
    }
}
