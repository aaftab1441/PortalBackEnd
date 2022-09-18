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
using Microsoft.AspNetCore.Cors;
using EaglePortal.Utils;

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesAgentController : Controller
    {
        private IConfiguration Configuration;
        private SalesAgentManager salesAgentManager;
        private UtilityManager utilityManager;
        public SalesAgentController(IConfiguration configuration, ILogger<SalesAgentController> logger)
        {
            this.Configuration = configuration;
            this.salesAgentManager = new SalesAgentManager(this.Configuration["ConnectionString"]);
            utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/Get")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Get(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesAgentManager.Get(user, itemData);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/Delete")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Delete(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesAgentManager.Delete(user, itemData);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }


        [HttpPost]
        [Route("~/[controller]/Save")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Save(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesAgentManager.Save(user, itemData);
                result = utilityManager.addToHashtable(returnResult, result);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/List")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> List(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("params", out itemData))
            {
                //Dictionary<string, object> returnResult = salesAgentManager.List(user, itemData);
                List<Dictionary<string, object>> returnResult = salesAgentManager.List(user, itemData);

                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }



    }

}



