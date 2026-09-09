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

        [JsonProperty("codigoNBS", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoNBS { get; set; }

        [JsonProperty("codigoTributacaoNacional", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoTributacaoNacional { get; set; }

        [JsonProperty("ibsCbs", NullValueHandling = NullValueHandling.Ignore)]
        public ServicoIbsCbs IbsCbs { get; set; }

        [JsonProperty("exportacao", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Exportacao { get; set; }

        [JsonProperty("regimeEspecialTributacao", NullValueHandling = NullValueHandling.Ignore)]
        public string RegimeEspecialTributacao { get; set; }

        [JsonProperty("tipoImunidadeIss", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoImunidadeIss { get; set; }

        [JsonProperty("paisPrestacaoServico", NullValueHandling = NullValueHandling.Ignore)]
        public string PaisPrestacaoServico { get; set; }

        [JsonProperty("ufPrestacaoServico", NullValueHandling = NullValueHandling.Ignore)]
        public string UfPrestacaoServico { get; set; }

        [JsonProperty("exigibilidadeSuspensa", NullValueHandling = NullValueHandling.Ignore)]
        public ExigibilidadeSuspensa ExigibilidadeSuspensa { get; set; }

        [JsonProperty("pisCofinsApuracaoPropria", NullValueHandling = NullValueHandling.Ignore)]
        public PisCofinsApuracaoPropria PisCofinsApuracaoPropria { get; set; }

        [JsonProperty("situacaoTributariaPisCofins", NullValueHandling = NullValueHandling.Ignore)]
        public string SituacaoTributariaPisCofins { get; set; }

        [JsonProperty("tipoRetencaoPisCofins", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoRetencaoPisCofins { get; set; }

        [JsonProperty("valorPis", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorPis { get; set; }

        [JsonProperty("valorCofins", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorCofins { get; set; }

        [JsonProperty("valorCsll", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorCsll { get; set; }

        [JsonProperty("valorInss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorInss { get; set; }

        [JsonProperty("valorIr", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ValorIr { get; set; }
    }
}
