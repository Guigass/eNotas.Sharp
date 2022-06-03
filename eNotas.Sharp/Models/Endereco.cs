using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Endereco
    {
        [JsonProperty("pais", NullValueHandling = NullValueHandling.Ignore)]
        public string Pais { get; set; }

        [JsonProperty("nomeDestinatario", NullValueHandling = NullValueHandling.Ignore)]
        public string NomeDestinatario { get; set; }

        [JsonProperty("uf", NullValueHandling = NullValueHandling.Ignore)]
        public string Uf { get; set; }

        [JsonProperty("cidade", NullValueHandling = NullValueHandling.Ignore)]
        public string Cidade { get; set; }

        [JsonProperty("logradouro", NullValueHandling = NullValueHandling.Ignore)]
        public string Logradouro { get; set; }

        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public string Numero { get; set; }

        [JsonProperty("complemento", NullValueHandling = NullValueHandling.Ignore)]
        public string Complemento { get; set; }

        [JsonProperty("bairro", NullValueHandling = NullValueHandling.Ignore)]
        public string Bairro { get; set; }

        [JsonProperty("cep", NullValueHandling = NullValueHandling.Ignore)]
        public string Cep { get; set; }

        [JsonProperty("tipoPessoaDestinatario", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoPessoaDestinatario { get; set; }

        [JsonProperty("cpfCnpjDestinatario", NullValueHandling = NullValueHandling.Ignore)]
        public string CpfCnpjDestinatario { get; set; }
    }
}
