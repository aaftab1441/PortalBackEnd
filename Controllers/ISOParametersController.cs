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
    public class ISOParametersController : Controller
    {
        private IConfiguration Configuration;
        private ISOParametersManager parameterManager;
        public ISOParametersController(IConfiguration configuration, ILogger<ISOParametersManager> logger)
        {
            this.Configuration = configuration;
            this.parameterManager = new ISOParametersManager(this.Configuration["ConnectionString"]);
        }

        [HttpPost]
        [Route("~/[controller]/GetISOParametersSearchData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetISOParametersSearchData(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement searchData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out searchData))
            {
                List<Dictionary<string, object>> searchResults = parameterManager.SearchISOParameters(user, searchData);
                result.Add("Success", true);
                result.Add("result", searchResults);
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/GetISOParametersDetail")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetISOParametersDetail(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement parameter;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("IsoParameter", out parameter))
            {
                Hashtable parameterDetail = parameterManager.GetISOParametersDetail(user, parameter);
                result.Add("Success", true);
                result.Add("result", parameterDetail);
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/GetPageData")]
        [Produces("application/json")]
        public ActionResult<Hashtable> GetPageData(JsonElement data)
        {
            Hashtable result = new Hashtable();

            JsonElement parameter, pageInfoJson;
            UtilityManager utilManager = new UtilityManager();

            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("isoParameter", out parameter) && data.TryGetProperty("pageInfo", out pageInfoJson))
            {
                Hashtable pageInfo = utilManager.GetHashMap(pageInfoJson);

                Hashtable isoParametersDetail = parameterManager.GetPageData(user, parameter.GetProperty("ISO_Code").ToString(), pageInfo);
                result.Add("Success", true);
                result.Add("result", isoParametersDetail);
            }
            return result;
        }

    }
}
