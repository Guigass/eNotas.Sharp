using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class IntermediadorTransacao
    {
        [JsonProperty("cnpjIntermediador", NullValueHandling = NullValueHandling.Ignore)]
        public string CnpjIntermediador { get; set; }

        [JsonProperty("idVendedorNoIntermediador", NullValueHandling = NullValueHandling.Ignore)]
        public string IdVendedorNoIntermediador { get; set; }
    }
}
