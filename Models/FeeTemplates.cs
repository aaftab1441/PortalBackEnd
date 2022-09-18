using Newtonsoft.Json;
using System.Collections.Generic;

namespace EaglePortal.Models
{
    public class FeeTemplates
    {
        public string success { get; set; }
        [JsonProperty("feeTemplates")]
        public List<FeeTemplate> Templates { get; set; }
    }
}
