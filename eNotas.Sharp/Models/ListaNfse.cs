using Newtonsoft.Json;
using System.Collections.Generic;

namespace eNotas.Sharp.Models
{
    public partial class ListaNfse
    {
        [JsonProperty("totalRecords", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalRecords { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<ConsultaNfse> Data { get; set; }
    }
}
