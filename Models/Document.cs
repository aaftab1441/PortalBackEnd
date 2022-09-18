using Newtonsoft.Json;

namespace EaglePortal.Models
{
    public class Document
    {
        [JsonProperty("document_type")]
        public string DocumentType { get; set; }
        [JsonProperty("upload_document")]
        public string Name { get; set; }
    }
}
