using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Transportadora
    {
        [JsonProperty("usarDadosEmitente", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UsarDadosEmitente { get; set; }

        [JsonProperty("tipoPessoa", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoPessoa { get; set; }

        [JsonProperty("cpfCnpj", NullValueHandling = NullValueHandling.Ignore)]
        public string CpfCnpj { get; set; }

        [JsonProperty("nome", NullValueHandling = NullValueHandling.Ignore)]
        public string Nome { get; set; }

        [JsonProperty("inscricaoEstadual", NullValueHandling = NullValueHandling.Ignore)]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("enderecoCompleto", NullValueHandling = NullValueHandling.Ignore)]
        public string EnderecoCompleto { get; set; }

        [JsonProperty("uf", NullValueHandling = NullValueHandling.Ignore)]
        public string Uf { get; set; }

        [JsonProperty("cidade", NullValueHandling = NullValueHandling.Ignore)]
        public string Cidade { get; set; }
    }

}
