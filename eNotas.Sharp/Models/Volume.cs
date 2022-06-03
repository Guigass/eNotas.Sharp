using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Volume
    {
        [JsonProperty("quantidade", NullValueHandling = NullValueHandling.Ignore)]
        public int? Quantidade { get; set; }

        [JsonProperty("especie", NullValueHandling = NullValueHandling.Ignore)]
        public string Especie { get; set; }

        [JsonProperty("marca", NullValueHandling = NullValueHandling.Ignore)]
        public string Marca { get; set; }

        [JsonProperty("numeracao", NullValueHandling = NullValueHandling.Ignore)]
        public string Numeracao { get; set; }

        [JsonProperty("pesoBruto", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PesoBruto { get; set; }

        [JsonProperty("pesoLiquido", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PesoLiquido { get; set; }
    }
}
