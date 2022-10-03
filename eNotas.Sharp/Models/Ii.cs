using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Ii
    {
        [JsonProperty("despesasAduaneiras", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DespesasAduaneiras { get; set; }

        [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Valor { get; set; }

        [JsonProperty("iof", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Iof { get; set; }
    }
}
