using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EaglePortal.Models
{
    public class FeeTemplate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }
        [JsonProperty("applicationtypeid")]
        public String ApplicationTypeId { get; set; }
        [JsonProperty("providerid")]
        public String ProviderId { get; set; }
        [JsonProperty("processorid")]
        public String ProcessorId { get; set; }
        [JsonProperty("pricetype")]
        public String PriceType { get; set; }
        [JsonProperty("userid")]
        public String UserId { get; set; }
        [JsonProperty("equipmentid")]
        public String EquipmentId { get; set; }
        [JsonProperty("pinpadid")]
        public String PinpadId { get; set; }
        [JsonProperty("checkreaderid")]
        public String CheckReaderId { get; set; }
        [JsonProperty("checkimagerid")]
        public String CheckImagerId { get; set; }
        [JsonProperty("possystemid")]
        public String PosSystemId { get; set; }
        [JsonProperty("paymentgatewayid")]
        public String PaymentGatewayId { get; set; }
        [JsonProperty("createduserid")]
        public String CreatedUserId { get; set; }
        [JsonProperty("updateduserid")]
        public String UpdatedUserId { get; set; }
        [JsonProperty("isdefault")]
        public String IsDefault { get; set; }
        [JsonProperty("agentdefault")]
        public String AgentDefault { get; set; }
        [JsonProperty("iscustom")]
        public String IsCustom { get; set; }
        [JsonProperty("created_at")]
        public String CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public String UpdatedAt { get; set; }
        [JsonProperty("categories")]
        public List<FeeCategory> Categories { get; set; }
    }
}
