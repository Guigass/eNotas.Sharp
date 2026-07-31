using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Servico
    {
        [JsonProperty("descricao", NullValueHandling = NullValueHandling.Ignore)]
        public string Descricao { get; set; }

        [JsonProperty("aliquotaIss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaIss { get; set; }

        [JsonProperty("issRetidoFonte", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IssRetidoFonte { get; set; }

        [JsonProperty("codigoServicoMunicipio", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoServicoMunicipio { get; set; }

        [JsonProperty("itemListaServicoLC116", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemListaServicoLC116 { get; set; }

        [JsonProperty("cnae", NullValueHandling = NullValueHandling.Ignore)]
        public string Cnae { get; set; }

        [JsonProperty("municipioPrestacaoServico", NullValueHandling = NullValueHandling.Ignore)]
        public string MunicipioPrestacaoServico { get; set; }
    }
}
