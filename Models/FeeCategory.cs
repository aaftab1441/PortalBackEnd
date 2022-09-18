using Newtonsoft.Json;
using System.Collections.Generic;

namespace EaglePortal.Models
{
    public class FeeCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }
    }
}
