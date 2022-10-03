using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Icms
    {
        [JsonProperty("situacaoTributaria", NullValueHandling = NullValueHandling.Ignore)]
        public string SituacaoTributaria { get; set; }

        [JsonProperty("origem", NullValueHandling = NullValueHandling.Ignore)]
        public int? Origem { get; set; }

        [JsonProperty("aliquota", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Aliquota { get; set; }

        [JsonProperty("baseCalculo", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BaseCalculo { get; set; }

        [JsonProperty("valor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Valor { get; set; }

        [JsonProperty("baseCalculoUFDestinoDifal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BaseCalculoUFDestinoDifal { get; set; }

        [JsonProperty("valorUFDestinoDifal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorUFDestinoDifal { get; set; }

        [JsonProperty("aliquotaUFDestinoDifal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaUFDestinoDifal { get; set; }

        [JsonProperty("aliquotaInterestadualDifal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaInterestadualDifal { get; set; }

        [JsonProperty("modalidadeBaseCalculo", NullValueHandling = NullValueHandling.Ignore)]
        public int? ModalidadeBaseCalculo { get; set; }

        [JsonProperty("percentualReducaoBaseCalculo", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualReducaoBaseCalculo { get; set; }

        [JsonProperty("baseCalculoST", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BaseCalculoSt { get; set; }

        [JsonProperty("aliquotaST", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaSt { get; set; }

        [JsonProperty("modalidadeBaseCalculoST", NullValueHandling = NullValueHandling.Ignore)]
        public int? ModalidadeBaseCalculoSt { get; set; }

        [JsonProperty("percentualReducaoBaseCalculoST", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualReducaoBaseCalculoSt { get; set; }

        [JsonProperty("percentualMargemValorAdicionadoST", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PercentualMargemValorAdicionadoSt { get; set; }
    }
}
