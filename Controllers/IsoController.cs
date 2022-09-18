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
    public class IsoController : Controller
    {
        private IConfiguration Configuration;
        private IsoManager isoManager;
        private UtilityManager utilityManager;
        public IsoController(IConfiguration configuration, ILogger<IsoController> logger)
        {
            this.Configuration = configuration;
            this.isoManager = new IsoManager(this.Configuration["ConnectionString"]);
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
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("iso", out itemData))
            {
                Dictionary<string, object> returnResult = isoManager.Get(user, itemData);
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
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("iso", out itemData))
            {
                Dictionary<string, object> returnResult = isoManager.Delete(user, itemData);
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
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("iso", out itemData))
            {
                Dictionary<string, object> returnResult = isoManager.Save(user, itemData);
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
                List<Dictionary<string, object>> returnResult = isoManager.List(user, @params);
                result.Add("Success", true);
                result.Add("result", returnResult);
            }
            return result;
        }



    }

}



