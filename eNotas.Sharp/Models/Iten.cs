using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Iten
    {
        [JsonProperty("cfop", NullValueHandling = NullValueHandling.Ignore)]
        public string Cfop { get; set; }

        [JsonProperty("codigo", NullValueHandling = NullValueHandling.Ignore)]
        public string Codigo { get; set; }

        [JsonProperty("descricao", NullValueHandling = NullValueHandling.Ignore)]
        public string Descricao { get; set; }

        [JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
        public string Sku { get; set; }

        [JsonProperty("ncm", NullValueHandling = NullValueHandling.Ignore)]
        public string Ncm { get; set; }

        [JsonProperty("cest", NullValueHandling = NullValueHandling.Ignore)]
        public string Cest { get; set; }

        [JsonProperty("quantidade", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Quantidade { get; set; }

        [JsonProperty("unidadeMedida", NullValueHandling = NullValueHandling.Ignore)]
        public string UnidadeMedida { get; set; }

        [JsonProperty("valorUnitario", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorUnitario { get; set; }

        [JsonProperty("outrasDespesas", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OutrasDespesas { get; set; }

        [JsonProperty("declaracaoImportacao", NullValueHandling = NullValueHandling.Ignore)]
        public DeclaracaoImportacao DeclaracaoImportacao { get; set; }

        [JsonProperty("frete", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Frete { get; set; }

        [JsonProperty("descontos", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Descontos { get; set; }

        [JsonProperty("impostos", NullValueHandling = NullValueHandling.Ignore)]
        public Impostos Impostos { get; set; }

        [JsonProperty("informacoesAdicionais", NullValueHandling = NullValueHandling.Ignore)]
        public string InformacoesAdicionais { get; set; }
    }
}
