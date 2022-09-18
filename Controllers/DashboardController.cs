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
    public class DashboardController : Controller
    {
        private IConfiguration Configuration;
        private DashboardManager dashboardManager;
        private SecurityManager securityManager;
        public DashboardController(IConfiguration configuration, ILogger<DashboardController> logger)
        {
            this.Configuration = configuration;
            this.securityManager = new SecurityManager(this.Configuration["ConnectionString"]);
            this.dashboardManager = new DashboardManager(this.Configuration["ConnectionString"]);
        }
        [HttpPost]
        [Route("~/[controller]/GetDashboardData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetDashboardData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions;
            JsonElement user, merchant, midElement;
            string mid = "";

            string userLevelCode;
            if (data.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions))
            {
                userLevelCode = permissions.GetProperty("User_Level_Code").GetString();
                if(userLevelCode == "DAS")
                {
                    result.Add("Data", dashboardManager.GetDashboardData());
                }
                else if(userLevelCode == "ISO")
                {
                    result.Add("Data", dashboardManager.GetMainIsoDashboardData(permissions.GetProperty("iso").GetString()));
                }
                else if(userLevelCode == "MERCHANT")
                {
                    
                    if(data.TryGetProperty("merchant", out merchant) && merchant.TryGetProperty("mm_cust_no", out midElement))
                    {
                        mid = midElement.GetString();
                    }
                    result.Add("Data", dashboardManager.GetMerchantDashboardData(user, mid ));
                }
                result.Add("Success", true);
                
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetIsoDetailDashboardData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetIsoDetailDashboardData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions;
            JsonElement user, iso;
            JsonElement isoCode;

            if (data.TryGetProperty("iso", out iso) && iso.TryGetProperty("ISO_CODE", out isoCode))
            {
                result.Add("Data", dashboardManager.GetIsoDetailDashboardData(iso.GetProperty("ISO_CODE").GetString()));
                result.Add("Success", true);
            }
            else if (data.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions))
            {
                result.Add("Data", dashboardManager.GetIsoDetailDashboardData(permissions.GetProperty("iso").GetString()));
                result.Add("Success", true);

            }else
            {
                result.Add("Success", false);
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/GetIsoMerchantCounts")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetIsoMerchantCounts(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions;
            JsonElement user, searchType;

            if (data.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions) && data.TryGetProperty("searchType", out searchType))
            {
                string[] parts = searchType.GetString().Split("/");
                result = dashboardManager.GetIsoMerchantCountsByStatus(parts);
                result.Add("Success", true);

            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetIsoListDashboardData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetIsoListDashboardData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement user;
            if (data.TryGetProperty("user", out user))
            {
                result.Add("Success", true);
                result.Add("Data", dashboardManager.GetIsoListDashboardData());
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetIsoDashboardData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetIsoDashboardData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement iso, isoCode;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("iso", out iso) && iso.TryGetProperty("ISO_CODE", out isoCode))
            {
                result.Add("Success", true);
                result.Add("Data", dashboardManager.GetMainIsoDashboardData(iso.GetProperty("ISO_CODE").GetString()));
            }

            return result;
        }

    }
}
