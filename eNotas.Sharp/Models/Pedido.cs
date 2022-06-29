using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Pedido
    {
        [JsonProperty("presencaConsumidor", NullValueHandling = NullValueHandling.Ignore)]
        public string PresencaConsumidor { get; set; }

        [JsonProperty("pagamento", NullValueHandling = NullValueHandling.Ignore)]
        public Pagamento Pagamento { get; set; }

        [JsonProperty("intermediadorTransacao", NullValueHandling = NullValueHandling.Ignore)]
        public IntermediadorTransacao IntermediadorTransacao { get; set; }
    }
}
