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
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration Configuration;
        private SecurityManager securityManager;
        public LoginController(IConfiguration configuration, ILogger<ConfigController> logger)
        {
            this.Configuration = configuration;
            this.securityManager = new SecurityManager(this.Configuration["ConnectionString"]);
        }
        [HttpPost]
        [Route("~/[controller]/Authenticate")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> Authenticate(JsonElement data)
        {
            Hashtable result = new Hashtable();
            // if(theParams != null && theParams.Length > 0)
            //  {
            //JObject data = JObject.Parse(theParams);
          
                Hashtable authenticated = securityManager.AuthenticateUser(data.GetProperty("user").GetProperty("emailId").GetString(), data.GetProperty("user").GetProperty("password").GetString());
                bool success = authenticated.Count > 0;
                if (success)
                {
                    result.Add("result", authenticated);
                }
                result.Add("Success", success);

          //  }
            return result;
        }



    }
}
