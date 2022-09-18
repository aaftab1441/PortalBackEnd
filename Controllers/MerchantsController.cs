using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EaglePortal.Services;
using System.Collections;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using EaglePortal.Utils;
using EaglePortal.Models;
using Newtonsoft.Json;

namespace EaglePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private IConfiguration Configuration;
        private MerchantManager merchantManager;
        private UtilityManager utilityManager;
        public MerchantsController(IConfiguration configuration, ILogger<MerchantsController> logger)
        {
            this.Configuration = configuration;
            this.merchantManager = new MerchantManager(this.Configuration["ConnectionString"]);
            this.utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/SaveMerchant")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> SaveMerchant(JsonElement data)
        {
            Hashtable result = new Hashtable();
            var merchant = JsonConvert.DeserializeObject<Merchant>(data.ToString());
            Dictionary<string, object> returnResult = merchantManager.SaveMerchant(merchant);
            result = utilityManager.addToHashtable(returnResult, result);
            return result;
        }
        [HttpGet]
        [Route("~/[controller]/GetMerchant")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetMerchant(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement merchant;
            if (data.TryGetProperty("merchant", out merchant))
            {
                JsonElement Merchant;
                if (merchant.TryGetProperty("Merchant", out Merchant))
                {
                    Dictionary<string, object> returnResult = merchantManager.GetMerchant(Convert.ToInt32(utilityManager.TryGetProperty(Merchant, "Id")));
                    result = utilityManager.addToHashtable(returnResult, result);
                    result.Add("Success", true);
                }
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/UpdateMerchant")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UpdateMerchant(JsonElement data)
        {
            Hashtable result = new Hashtable();
            var merchant = JsonConvert.DeserializeObject<Merchant>(data.ToString());
            Dictionary<string, object> returnResult = merchantManager.UpdateMerchant(merchant);
            result = utilityManager.addToHashtable(returnResult, result);
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/UpdateIsoInformation")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UpdateIsoInformation(JsonElement data)
        {
            Hashtable result = new Hashtable();
            var isoInformation = JsonConvert.DeserializeObject<IsoInformation>(data.ToString());

            Dictionary<string, object> returnResult = merchantManager.UpdateIsoInformation(isoInformation);
            result = utilityManager.addToHashtable(returnResult, result);

            return result;
        }

        [HttpGet]
        [Route("~/[controller]/GetISOInformation")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetISOInformation(JsonElement data)
        {
            Hashtable result = new Hashtable();
            string id = JsonConvert.DeserializeObject<string>(data.ToString()); ;
            if (!string.IsNullOrEmpty(id))
            {
                Dictionary<string, object> returnResult = merchantManager.GetISOInfo(Convert.ToInt32(id));
                result = utilityManager.addToHashtable(returnResult, result);
                result.Add("Success", true);
            }
            return result;
        }
    }
}
