using Newtonsoft.Json;
using System.Collections.Generic;

namespace eNotas.Sharp.Models
{
    public partial class ListaEmpresas
    {
        [JsonProperty("totalRecords", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalRecords { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Empresa> Data { get; set; }
    }
}
