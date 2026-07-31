using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Empresa
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("cnpj", NullValueHandling = NullValueHandling.Ignore)]
        public string Cnpj { get; set; }

        [JsonProperty("inscricaoMunicipal", NullValueHandling = NullValueHandling.Ignore)]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("inscricaoEstadual", NullValueHandling = NullValueHandling.Ignore)]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("razaoSocial", NullValueHandling = NullValueHandling.Ignore)]
        public string RazaoSocial { get; set; }

        [JsonProperty("nomeFantasia", NullValueHandling = NullValueHandling.Ignore)]
        public string NomeFantasia { get; set; }

        [JsonProperty("optanteSimplesNacional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OptanteSimplesNacional { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("telefoneComercial", NullValueHandling = NullValueHandling.Ignore)]
        public string TelefoneComercial { get; set; }

        [JsonProperty("incentivadorCultural", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IncentivadorCultural { get; set; }

        [JsonProperty("endereco", NullValueHandling = NullValueHandling.Ignore)]
        public Endereco Endereco { get; set; }

        [JsonProperty("regimeEspecialTributacao", NullValueHandling = NullValueHandling.Ignore)]
        public string RegimeEspecialTributacao { get; set; }

        [JsonProperty("codigoServicoMunicipal", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoServicoMunicipal { get; set; }

        [JsonProperty("itemListaServicoLC116", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemListaServicoLC116 { get; set; }

        [JsonProperty("cnae", NullValueHandling = NullValueHandling.Ignore)]
        public string Cnae { get; set; }

        [JsonProperty("aliquotaIss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AliquotaIss { get; set; }

        [JsonProperty("descricaoServico", NullValueHandling = NullValueHandling.Ignore)]
        public string DescricaoServico { get; set; }

        [JsonProperty("enviarEmailCliente", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnviarEmailCliente { get; set; }

        [JsonProperty("configuracoesNFSeHomologacao", NullValueHandling = NullValueHandling.Ignore)]
        public ConfiguracoesNfse ConfiguracoesNfseHomologacao { get; set; }

        [JsonProperty("configuracoesNFSeProducao", NullValueHandling = NullValueHandling.Ignore)]
        public ConfiguracoesNfse ConfiguracoesNfseProducao { get; set; }
    }
}
