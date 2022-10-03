using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace eNotas.Sharp.Models
{
    public partial class DeclaracaoImportacao
    {
        [JsonProperty("numero", NullValueHandling = NullValueHandling.Ignore)]
        public string Numero { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? Data { get; set; }

        [JsonProperty("localDesembaraco", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalDesembaraco { get; set; }

        [JsonProperty("ufDesembaraco", NullValueHandling = NullValueHandling.Ignore)]
        public string UfDesembaraco { get; set; }

        [JsonProperty("dataDesembaraco", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTimeOffset? DataDesembaraco { get; set; }

        [JsonProperty("tipoViaTransporte", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoViaTransporte { get; set; }

        [JsonProperty("valorAFRMM", NullValueHandling = NullValueHandling.Ignore)]
        public double? ValorAfrmm { get; set; }

        [JsonProperty("tipoIntermedio", NullValueHandling = NullValueHandling.Ignore)]
        public string TipoIntermedio { get; set; }

        [JsonProperty("cnpj", NullValueHandling = NullValueHandling.Ignore)]
        public string Cnpj { get; set; }

        [JsonProperty("ufTerceiro", NullValueHandling = NullValueHandling.Ignore)]
        public string UfTerceiro { get; set; }

        [JsonProperty("codigoExportador", NullValueHandling = NullValueHandling.Ignore)]
        public string CodigoExportador { get; set; }

        [JsonProperty("adicoes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Adicoe> Adicoes { get; set; }
    }
}
