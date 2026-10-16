using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class ItemReferenciado
    {
        [JsonProperty("chaveAcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string ChaveAcesso { get; set; }
        [JsonProperty("numeroItem", NullValueHandling = NullValueHandling.Ignore)]
        public int NumeroItem { get; set; }
    }
}
