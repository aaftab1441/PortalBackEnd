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
    public class SalesOfficeController : Controller
    {
        private IConfiguration Configuration;
        private SalesOfficeManager salesOfficeManager;
        private UtilityManager utilityManager;
        public SalesOfficeController(IConfiguration configuration, ILogger<SalesOfficeController> logger)
        {
            this.Configuration = configuration;
            this.salesOfficeManager = new SalesOfficeManager(this.Configuration["ConnectionString"]);
            utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/get")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Get(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesOfficeManager.Get(user, itemData);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/delete")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Delete(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesOfficeManager.Delete(user, itemData);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }


        [HttpPost]
        [Route("~/[controller]/save")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Save(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement itemData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("item", out itemData))
            {
                Dictionary<string, object> returnResult = salesOfficeManager.Save(user, itemData);
                result = utilityManager.addToHashtable(returnResult, result);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/list")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> List(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement @params;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("params", out @params))
            {
                List<Dictionary<string, object>> returnResult = salesOfficeManager.List(user, @params);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }



    }

}


