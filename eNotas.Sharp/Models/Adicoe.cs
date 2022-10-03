using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Adicoe
    {
        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public int? Numero { get; set; }

        [JsonProperty("codigoFabricante", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoFabricante { get; set; }

        [JsonProperty("numeroDrawback", NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroDrawback { get; set; }

        [JsonProperty("descontos")]
        public object Descontos { get; set; }
    }
}
