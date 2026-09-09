using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class ExigibilidadeSuspensa
    {
        [JsonProperty("tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string Tipo { get; set; }

        [JsonProperty("numeroProcesso", NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroProcesso { get; set; }
    }
}
