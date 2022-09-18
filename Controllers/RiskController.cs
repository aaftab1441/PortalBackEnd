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
    public class RiskController : Controller
    {
        private IConfiguration Configuration;
        private RiskManager riskManager;
        private SecurityManager securityManager;
        private UtilityManager utilityManager;
        public RiskController(IConfiguration configuration, ILogger<DashboardController> logger)
        {
            this.Configuration = configuration;
            this.securityManager = new SecurityManager(this.Configuration["ConnectionString"]);
            this.riskManager = new RiskManager(this.Configuration["ConnectionString"]);
            this.utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/Get13MonthHistory")]
        [Produces("application/json")]
        public ActionResult<Hashtable> Get13MonthHistory(JsonElement parameters)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions, orgCodeElement;
            JsonElement user, merchant, dataObject, dataContainer, dataType;
            string userLevelCode;
            string orgCode = "";
            string objectType;
            if (parameters.TryGetProperty("data", out dataContainer) && dataContainer.TryGetProperty("data", out dataObject) &&
                dataContainer.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions) &&
                dataContainer.TryGetProperty("dataType", out dataType))
            {

                objectType = dataType.GetString();
                //userLevelCode = permissions.GetProperty("User_Level_Code").GetString();
                if (objectType == "EAGLE")
                {
                    result.Add("Data", riskManager.Get13MonthHistory());
                    result.Add("Detail", riskManager.GetIsoDetail("0001"));
                    
                }
                else if (objectType == "ISO")
                {
                    if(permissions.TryGetProperty("iso", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();

                    }
                    if (dataObject.TryGetProperty("ISO_CODE", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();
                    }
                    result.Add("Data", riskManager.Get13MonthHistory(objectType, orgCode));
                    result.Add("Detail", riskManager.GetIsoDetail(orgCode));
                }
                else if (objectType == "MERCHANT")
                {

                    if (permissions.TryGetProperty("mid", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();

                    }
                    if (dataObject.TryGetProperty("mm_cust_no", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();
                    }
                    result.Add("Data", riskManager.Get13MonthHistory("MCT", orgCode));
                    
                    result.Add("Detail", riskManager.GetMerchantDetail(orgCode));
                }
                result.Add("Success", true);

                 
            }
            return result;
        }


        [HttpPost]
        [Route("~/[controller]/GetMerchantReserve")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetMerchantReserve(JsonElement parameters)
        {
            Hashtable result = new Hashtable();
            JsonElement permissions, orgCodeElement;
            JsonElement user,  dataObject, dataContainer, dataType;
            string orgCode = "";
            string objectType;
            if (parameters.TryGetProperty("data", out dataContainer) && dataContainer.TryGetProperty("data", out dataObject) &&
                dataContainer.TryGetProperty("user", out user) && user.TryGetProperty("Permissions", out permissions) &&
                dataContainer.TryGetProperty("dataType", out dataType))
            {

                objectType = dataType.GetString();
                //userLevelCode = permissions.GetProperty("User_Level_Code").GetString();
                if (objectType == "EAGLE")
                {
                    result.Add("Data", riskManager.GetMerchantReserve("ISO", "0001"));
                    result.Add("Detail", riskManager.GetIsoDetail("0001"));

                }
                else if (objectType == "ISO")
                {
                    if (permissions.TryGetProperty("iso", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();

                    }
                    if (dataObject.TryGetProperty("ISO_CODE", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();
                    }
                    result.Add("Data", riskManager.GetMerchantReserve(objectType, orgCode));
                    result.Add("Detail", riskManager.GetIsoDetail(orgCode));
                }
                else if (objectType == "MERCHANT")
                {

                    if (permissions.TryGetProperty("mid", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();

                    }
                    if (dataObject.TryGetProperty("mm_cust_no", out orgCodeElement))
                    {
                        orgCode = orgCodeElement.GetString();
                    }
                    result.Add("Data", riskManager.GetMerchantReserve("MCT", orgCode));
                    result.Add("Detail", riskManager.GetMerchantDetail(orgCode));
                }
                result.Add("Success", true);


            }
            return result;
        }

    }
}
