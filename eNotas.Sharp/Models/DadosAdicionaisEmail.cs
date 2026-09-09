using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class DadosAdicionaisEmail
    {
        [JsonProperty("outrosDestinatarios", NullValueHandling = NullValueHandling.Ignore)]
        public string OutrosDestinatarios { get; set; }
    }
}
