using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class CredenciadoraCartao
    {
        [JsonProperty("tipoIntegracaoPagamento", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoIntegracaoPagamento { get; set; }

        [JsonProperty("cnpjCredenciadoraCartao", NullValueHandling = NullValueHandling.Ignore)]
        public string CnpjCredenciadoraCartao { get; set; }

        [JsonProperty("bandeira", NullValueHandling = NullValueHandling.Ignore)]
        public string Bandeira { get; set; }

        [JsonProperty("autorizacao", NullValueHandling = NullValueHandling.Ignore)]
        public string Autorizacao { get; set; }
    }
}
