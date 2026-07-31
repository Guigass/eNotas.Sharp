using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class IbsCbs
    {
        [JsonProperty("situacaoTributaria", NullValueHandling = NullValueHandling.Ignore)]
        public string SituacaoTributaria { get; set; }

        [JsonProperty("classificacaoTributaria", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassificacaoTributaria { get; set; }

        [JsonProperty("ibs", NullValueHandling = NullValueHandling.Ignore)]
        public Ibs Ibs { get; set; }

        [JsonProperty("cbs", NullValueHandling = NullValueHandling.Ignore)]
        public Cbs Cbs { get; set; }
    }

    public partial class Ibs
    {
        [JsonProperty("uf", NullValueHandling = NullValueHandling.Ignore)]
        public IbsUf Uf { get; set; }

        [JsonProperty("municipio", NullValueHandling = NullValueHandling.Ignore)]
        public IbsMunicipio Municipio { get; set; }
    }

    public partial class IbsUf
    {
        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }

        [JsonProperty("percentualDiferimento", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualDiferimento { get; set; }

        [JsonProperty("percentualReducaoAliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualReducaoAliquota { get; set; }
    }

    public partial class IbsMunicipio
    {
        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }
    }

    public partial class Cbs
    {
        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }

        [JsonProperty("percentualDiferimento", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualDiferimento { get; set; }

        [JsonProperty("percentualReducaoAliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualReducaoAliquota { get; set; }
    }
}
