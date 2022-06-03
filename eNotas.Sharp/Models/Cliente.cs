using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Cliente
    {
        [JsonProperty("endereco", NullValueHandling = NullValueHandling.Ignore)]
        public Endereco Endereco { get; set; }

        [JsonProperty("tipoPessoa", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoPessoa { get; set; }

        [JsonProperty("nome", NullValueHandling = NullValueHandling.Ignore)]
        public string Nome { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("cpfCnpj", NullValueHandling = NullValueHandling.Ignore)]
        public string CpfCnpj { get; set; }

        [JsonProperty("inscricaoMunicipal", NullValueHandling = NullValueHandling.Ignore)]
        public string InscricaoMunicipal { get; set; }

        [JsonProperty("inscricaoEstadual", NullValueHandling = NullValueHandling.Ignore)]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("indicadorContribuinteICMS", NullValueHandling = NullValueHandling.Ignore)]
        public string IndicadorContribuinteIcms { get; set; }

        [JsonProperty("telefone", NullValueHandling = NullValueHandling.Ignore)]
        public string Telefone { get; set; }
    }
}
