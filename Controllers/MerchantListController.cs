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

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MerchantListController : Controller
    {
        private IConfiguration Configuration;
        private MerchantListMangaer merchantListManager;
        public MerchantListController(IConfiguration configuration, ILogger<MerchantListController> logger)
        {
            this.Configuration = configuration;
            this.merchantListManager = new MerchantListMangaer(this.Configuration["ConnectionString"]);
        }
        [HttpPost]
        [Route("~/[controller]/GetMerchantsByStatus")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetMerchantsByStatus(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions;
            JsonElement user;
            string[] parts;
            if (data.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions))
            {
                parts = data.GetProperty("searchType").GetString().Split("/");
                result = merchantListManager.GetMerchantsByStatus(parts, data.GetProperty("searchParams"));
                result.Add("Success", true);
            }
            return result;
        }
 

        
    }
}
