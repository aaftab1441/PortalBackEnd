using EaglePortal.Models;
using EaglePortal.Services;
using EaglePortal.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace EaglePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private IConfiguration Configuration;
        private OwnerManager ownermanager;
        private UtilityManager utilityManager;
        public OwnerController(IConfiguration configuration, ILogger<OwnerController> logger)
        {
            this.Configuration = configuration;
            this.ownermanager = new OwnerManager(this.Configuration["ConnectionString"]);
            this.utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/UpdateOwner")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UpdateOwner(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement owners;
            JsonElement merchantId;

            if (data.TryGetProperty("owners", out owners) && data.TryGetProperty("merchantId", out merchantId))
            {
                var ownerList = JsonConvert.DeserializeObject<List<Owner>>(owners.ToString());
                if (ownerList.Count() > 0)
                {
                    Dictionary<string, object> returnResult = ownermanager.UpsertOwner(ownerList, merchantId.ToString());
                    result = utilityManager.addToHashtable(returnResult, result);
                }
            }
            return result;
        }
    }
}
