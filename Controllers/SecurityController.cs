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

namespace EaglePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        private IConfiguration Configuration;
        private SecurityManager securityManager;
        private MerchantSearchManager merchantSearchManager;
        public SecurityController(IConfiguration configuration, ILogger<SecurityController> logger)
        {
            this.Configuration = configuration;
            this.securityManager = new SecurityManager(this.Configuration["ConnectionString"]);
            this.merchantSearchManager = new MerchantSearchManager(this.Configuration["ConnectionString"]);
        }
        [HttpPost]
        [Route("~/[controller]/GetUserList")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetUserList(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement searchData;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out searchData))
            {
                List<Dictionary<string, object>> searchResults = securityManager.SearchUsers(user, searchData);
                result.Add("Success", true);
                result.Add("result", searchResults);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/DeleteUserAccess")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> DeleteUserAccess(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser, currentMerchant;
            JsonElement user;
            JsonElement searchData = new JsonElement();
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("currentUser", out currentUser) && data.TryGetProperty("currentMerchant", out currentMerchant))
            {
                Hashtable access = securityManager.DeleteUserAccess(user, currentUser, currentMerchant);
                result.Add("Success", true);
                result.Add("result", access);
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/EnableUserAccess")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> EnableUserAccess(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser, currentMerchant;
            JsonElement user;
            JsonElement searchData = new JsonElement();
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("currentUser", out currentUser) && data.TryGetProperty("currentMerchant", out currentMerchant))
            {
                Hashtable access = securityManager.EnableUserAccess(user, currentUser, currentMerchant);
                result.Add("Success", true);
                result.Add("result", access);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetUserDetails")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetUserDetails(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser;
            JsonElement user;
            JsonElement searchData = new JsonElement();
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out currentUser))
            {
                Hashtable userDetail = securityManager.GetUserDetail(user, currentUser);
                List<Dictionary<string, object>> searchResults = merchantSearchManager.SearchMerchants(user, searchData);
                result.Add("merchants", searchResults);
                result.Add("Success", true);
                result.Add("result", userDetail);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/GetMyUserDetails")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetMyUserDetails(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out currentUser))
            {
                Hashtable userDetail = securityManager.GetMyUserDetail(user, currentUser);
                result.Add("Success", true);
                result.Add("result", userDetail);
            }
            return result;
        }

        [HttpPost]
        [Route("~/[controller]/DeleteUser")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> DeleteUser(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("currentUser", out currentUser))
            {
                Hashtable userDetail = securityManager.DeleteUser(user, currentUser);
                result.Add("Success", true);
                result.Add("result", userDetail);
            }
            return result;
        }


        [HttpPost]
        [Route("~/[controller]/SubmitUserDetails")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> SubmitUserDetails(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement currentUser;
            JsonElement user;
            if (data.TryGetProperty("user", out user) && data.TryGetProperty("data", out currentUser))
            {
                securityManager.SubmitUserDetail(user, currentUser);
                Hashtable userDetail = securityManager.GetUserDetail(user, currentUser.GetProperty("UserDetails"));
                result.Add("Success", true);
                result.Add("result", userDetail);
            }
            return result;
        }

    }


}


