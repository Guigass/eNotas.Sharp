using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class Nota
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("ambienteEmissao", NullValueHandling = NullValueHandling.Ignore)]
        public string AmbienteEmissao { get; set; }

        [JsonProperty("naturezaOperacao", NullValueHandling = NullValueHandling.Ignore)]
        public string NaturezaOperacao { get; set; }

        [JsonProperty("tipoOperacao", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoOperacao { get; set; }

        [JsonProperty("finalidade", NullValueHandling = NullValueHandling.Ignore)]
        public string Finalidade { get; set; }

        [JsonProperty("consumidorFinal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ConsumidorFinal { get; set; }

        [JsonProperty("enviarPorEmail", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnviarPorEmail { get; set; }

        [JsonProperty("nfeReferenciada", NullValueHandling = NullValueHandling.Ignore)]
        public List<NfeReferenciada> NfeReferenciada { get; set; }

        [JsonProperty("dataEmissao", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataEmissao { get; set; }

        [JsonProperty("pedido", NullValueHandling = NullValueHandling.Ignore)]
        public Pedido Pedido { get; set; }

        [JsonProperty("transporte", NullValueHandling = NullValueHandling.Ignore)]
        public Transporte Transporte { get; set; }

        [JsonProperty("cliente", NullValueHandling = NullValueHandling.Ignore)]
        public Cliente Cliente { get; set; }

        [JsonProperty("itens", NullValueHandling = NullValueHandling.Ignore)]
        public List<Iten> Itens { get; set; }

        [JsonProperty("informacoesAdicionais", NullValueHandling = NullValueHandling.Ignore)]
        public string InformacoesAdicionais { get; set; }

        [JsonProperty("informacoesAdicionaisFisco", NullValueHandling = NullValueHandling.Ignore)]
        public string InformacoesAdicionaisFisco { get; set; }
    }
}
