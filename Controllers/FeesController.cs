using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    public class FeesController : Controller
    {
        private IConfiguration Configuration;
        private FeesManager feesManager;
        private string base_API_Path;
        private UtilityManager utilityManager;

        public FeesController(IConfiguration configuration, ILogger<FeesController> logger)
        {
            this.Configuration = configuration;
            this.feesManager = new FeesManager(this.Configuration["ConnectionString"]);
            this.base_API_Path = this.Configuration["BASE_API_PATH"];
            this.utilityManager = new UtilityManager();
        }
       

        [HttpPost]
        [Route("~/[controller]/Getfees")]
        [Produces("application/json")]
        public ActionResult<Hashtable> Getfees(JsonElement data)
        {
            JsonElement user;
            Hashtable result = new Hashtable();

            if (data.TryGetProperty("user", out user))
            {
                Dictionary<string, object> returnResult = feesManager.GetFeesAsync(user, base_API_Path);
                result = utilityManager.addToHashtable(returnResult, result);
            }
            
            return result;
        }
    }
}
