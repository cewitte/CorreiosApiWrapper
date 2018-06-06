using Newtonsoft.Json;
using System.Collections.Generic;

namespace CorreiosApiWrapper.Business
{
    public class CorreiosServicePrices
    {
        [JsonProperty("cServico")]
        public List<CorreiosServices> CorreiosServices { get; set; }
    }
}
