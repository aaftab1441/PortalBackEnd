using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EaglePortal.Services;
using System.Collections;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using EaglePortal.Utils;
using EaglePortal.Models;
using Newtonsoft.Json;

namespace EaglePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private IConfiguration Configuration;
        private ApplicationManager applicationManager;
        private UtilityManager utilityManager;
        private string base_API_Path;

        public ApplicationController(IConfiguration configuration, ILogger<ApplicationController> logger)
        {
            this.Configuration = configuration;
            this.applicationManager = new ApplicationManager(this.Configuration["ConnectionString"]);
            this.base_API_Path = this.Configuration["BASE_API_PATH"];
            this.utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/AddApplication")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> AddApplication(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement template;
            JsonElement merchantId;
            JsonElement application;

            if (data.TryGetProperty("template", out template) && data.TryGetProperty("merchantId", out merchantId) && data.TryGetProperty("application", out application))
            {
                var feeTemplate = JsonConvert.DeserializeObject<FeeTemplate>(template.ToString());
                Dictionary<string, object> returnResult = applicationManager.SaveApplication(application, feeTemplate, base_API_Path);
                result = utilityManager.addToHashtable(returnResult, result);
            }

            return result;
        }
        [HttpGet]
        [Route("~/[controller]/GetApplication")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetApplication(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement application;
            if (data.TryGetProperty("application", out application))
            {
                JsonElement Application;
                if (application.TryGetProperty("Application", out Application))
                {
                    Dictionary<string, object> returnResult = applicationManager.GetApplication(Convert.ToInt32(utilityManager.TryGetProperty(Application, "Id")));
                    result = utilityManager.addToHashtable(returnResult, result);
                    result.Add("Success", true);
                }
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/UpdateApplication")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UpdateApplication(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement application;
            if (data.TryGetProperty("application", out application))
            {
                JsonElement Application;
                if (application.TryGetProperty("Application", out Application))
                {
                    Dictionary<string, object> returnResult = applicationManager.UpdateApplication(Application);
                    result = utilityManager.addToHashtable(returnResult, result);
                    result.Add("Success", true);
                }
            }
            return result;
        }
    }
}
