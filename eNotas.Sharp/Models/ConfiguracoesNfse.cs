using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class ConfiguracoesNfse
    {
        [JsonProperty("sequencialNFe", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequencialNFe { get; set; }

        [JsonProperty("serieNFe", NullValueHandling = NullValueHandling.Ignore)]
        public string SerieNFe { get; set; }

        [JsonProperty("sequencialLoteNFe", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequencialLoteNFe { get; set; }

        [JsonProperty("usuarioAcessoProvedor", NullValueHandling = NullValueHandling.Ignore)]
        public string UsuarioAcessoProvedor { get; set; }

        [JsonProperty("senhaAcessoProvedor", NullValueHandling = NullValueHandling.Ignore)]
        public string SenhaAcessoProvedor { get; set; }

        [JsonProperty("tokenAcessoProvedor", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenAcessoProvedor { get; set; }
    }
}
