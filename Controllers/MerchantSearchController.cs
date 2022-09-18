using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EaglePortal.Services;
using System.Collections;
using System.Text.Json;
using EaglePortal.Utils;

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantSearchController : Controller
    {
        private IConfiguration Configuration;
        private MerchantSearchManager merchantSearchManager;
        public MerchantSearchController(IConfiguration configuration, ILogger<MerchantSearchController> logger)
        {
            this.Configuration = configuration;
            this.merchantSearchManager = new MerchantSearchManager(this.Configuration["ConnectionString"]);
        }
        [HttpPost]
        [Route("~/[controller]/GetMerchantSearchData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetMerchantsByStatus(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement searchData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out searchData))
            {
                List<Dictionary<string, object>> searchResults = merchantSearchManager.SearchMerchants(user, searchData);
                result.Add("Success", true);
                result.Add("result", searchResults);
            }
            return result;
        }
        
        [HttpPost]
        [Route("~/[controller]/GetMerchantDetail")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetMerchantDetail(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement merchant;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("merchant", out merchant))
            {
                Hashtable merchantDetail = merchantSearchManager.GetMerchantDetail(user, merchant);
                result.Add("Success", true);
                result.Add("result", merchantDetail);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetMerchantMaintainDetail")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetMerchantMaintainDetail(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement merchant;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("merchant", out merchant))
            {
                Hashtable merchantDetail = merchantSearchManager.GetMerchantMaintainDetail(user, merchant);
                result.Add("Success", true);
                result.Add("result", merchantDetail);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetPageData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetPageData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            
            JsonElement merchant, pageInfoJson;
            UtilityManager utilManager = new UtilityManager();

            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("merchant", out merchant) && data.TryGetProperty("pageInfo", out pageInfoJson))
            {
                Hashtable pageInfo = utilManager.GetHashMap(pageInfoJson);

                Hashtable merchantDetail = merchantSearchManager.GetPageData(user, merchant.GetProperty("mm_cust_no").ToString(), pageInfo);
                result.Add("Success", true);
                result.Add("result", merchantDetail);
            }
            return result;
        }

    }
}
