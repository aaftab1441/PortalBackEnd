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

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : Controller
    {
        private IConfiguration Configuration;
        private ConfigManager configManager;
        public ConfigController(IConfiguration configuration, ILogger<ConfigController> logger)
        {
            this.Configuration = configuration;
            this.configManager = new ConfigManager(this.Configuration["ConnectionString"]);
        }
        [HttpGet]
        [Route("~/[controller]/list")]
        [Produces("application/json")]
        public ActionResult<Hashtable> List()
        {
            Hashtable result = new Hashtable();
            result.Add("Lists", configManager.ListConfig());
            result.Add("Success", true);
            return result;
        }
 

        
    }
}
