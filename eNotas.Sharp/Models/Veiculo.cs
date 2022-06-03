using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Veiculo
    {
        [JsonProperty("placa", NullValueHandling = NullValueHandling.Ignore)]
        public string Placa { get; set; }

        [JsonProperty("uf", NullValueHandling = NullValueHandling.Ignore)]
        public string Uf { get; set; }
    }
}
