using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class NfeReferenciada
    {
        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }
        [JsonProperty("numeroItem", NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroItem { get; set; }
    }
}
