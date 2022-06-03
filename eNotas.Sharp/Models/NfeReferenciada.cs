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
    }
}
