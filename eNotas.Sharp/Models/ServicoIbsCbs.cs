using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class ServicoIbsCbs
    {
        [JsonProperty("classificacaoTributaria", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassificacaoTributaria { get; set; }

        [JsonProperty("codigoIndicadorOperacao", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoIndicadorOperacao { get; set; }
    }
}
