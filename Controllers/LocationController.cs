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
using Newtonsoft.Json;
using EaglePortal.Models;
using System.IO;

namespace EaglePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private IConfiguration Configuration;
        private LocationManager locationManager;
        private UtilityManager utilityManager;
        public LocationController(IConfiguration configuration, ILogger<LocationController> logger)
        {
            this.Configuration = configuration;
            this.locationManager = new LocationManager(this.Configuration["ConnectionString"]);
            this.utilityManager = new UtilityManager();
        }
        [HttpPost]
        [Route("~/[controller]/AddLocation")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> AddLocation(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement locationsJson;
            JsonElement merchantId;

            if (data.TryGetProperty("locations", out locationsJson) && data.TryGetProperty("merchantId", out merchantId))
            {
                Dictionary<string, object> toReturn = new Dictionary<string, object>();
                var locations = JsonConvert.DeserializeObject<List<Location>>(locationsJson.ToString());

                foreach (var location in locations)
                {
                    location.Merchantid = merchantId.ToString();
                    locationManager.SaveLocation(location);
                }

                toReturn.Add("Success", Convert.ToInt32(merchantId.ToString()));
                result = utilityManager.addToHashtable(toReturn, result);
            }
            return result;
        }
        [HttpGet]
        [Route("~/[controller]/GetLocation")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> GetLocation(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement location;
            if (data.TryGetProperty("location", out location))
            {
                JsonElement Location;
                if (location.TryGetProperty("Location", out Location))
                {
                    Dictionary<string, object> returnResult = locationManager.GetLocation(Convert.ToInt32(utilityManager.TryGetProperty(Location, "Id")));
                    result = utilityManager.addToHashtable(returnResult, result);
                    result.Add("Success", true);
                }
            }
            return result;
        }
        [HttpPost]
        [Route("~/[controller]/UpdateLocation")]
        [Produces("application/json")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UpdateLocation(JsonElement data)
        {
            Hashtable result = new Hashtable();
            JsonElement location;
            if (data.TryGetProperty("location", out location))
            {
                JsonElement Location;
                if (location.TryGetProperty("Location", out Location))
                {
                    Dictionary<string, object> returnResult = locationManager.UpdateLocation(Location);
                    result = utilityManager.addToHashtable(returnResult, result);
                    result.Add("Success", true);
                }
            }
            return result;

        }

        [HttpPost]
        [Route("~/[controller]/UploadDocument")]
        [EnableCors("AnyOrigin")]
        public ActionResult<Hashtable> UploadDocument([FromForm] LocationDocument file)
        {
            Hashtable result = new Hashtable();
            try
            {
                Random rng = new Random();
                int number = rng.Next(1, 1000000000);
                string digits = number.ToString("000000000");

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PF" + digits);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }

                result.Add("Success", true);
            }
            catch (Exception ex)
            {
                result.Add("Success", false);
            }

            return result;
        }
    }
}
