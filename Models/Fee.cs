using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EaglePortal.Models
{
	public class Fee
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("parentid")]
		public String ParentId { get; set; }
		[JsonProperty("applicationtypeid")]
		public String ApplicationTypeId { get; set; }
		[JsonProperty("section")]
		public String Section { get; set; }
		[JsonProperty("templateid")]
		public String TemplateId { get; set; }
		[JsonProperty("name")]
		public String Name { get; set; }
		[JsonProperty("fieldtype")]
		public String FieldType { get; set; }
		[JsonProperty("valuetype")]
		public String ValueType { get; set; }
		[JsonProperty("defaultvalue")]
		public String DefaultValue { get; set; }
		[JsonProperty("highvalue")]
		public String HighValue { get; set; }
		[JsonProperty("lowvalue")]
		public String LowValue { get; set; }
		[JsonProperty("columntodisplay")]
		public String ColumnToDisplay { get; set; }
		[JsonProperty("leftdisplayorder")]
		public String LeftDisplayOrder { get; set; }
		[JsonProperty("rightdisplayorder")]
		public String RightDisplayOrder { get; set; }
		[JsonProperty("description")]
		public String Description { get; set; }
		[JsonProperty("percent")]
		public String Percent { get; set; }
		[JsonProperty("selectoptions")]
		public string SelectOptions { get; set; }
		[JsonProperty("haspercent")]
		public String HasPercent { get; set; }
		[JsonProperty("feetier")]
		public String FeeTier { get; set; }
		[JsonProperty("userid")]
		public String UserId { get; set; }
		[JsonProperty("isdefault")]
		public String IsDefault { get; set; }
		[JsonProperty("voidflag")]
		public String VoidFlag { get; set; }
		[JsonProperty("created_at")]
		public String CreatedAt { get; set; }
		[JsonProperty("updated_at")]
		public String UpdatedAt { get; set; }

	}
}
