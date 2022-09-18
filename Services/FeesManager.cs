using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using EaglePortal.Models;

namespace EaglePortal.Services
{
    public class FeesManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public FeesManager(string connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~FeesManager()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public Dictionary<string, object> GetFeesAsync(JsonElement userObj, string base_API_Path)
        {
            //Get API Token
            string url = base_API_Path + "getapitoken";

            JsonElement userDetails = userObj.GetProperty("UserDetail");
            JsonElement email;
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            if (userDetails.TryGetProperty("Email_ID", out email))
            {
                var model = new { email = email.GetString() };
                try
                {
                    HttpClient client = new HttpClient();
                    var response = client.PostAsJsonAsync(url, model).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        dynamic data = JsonConvert.DeserializeObject(json);
                        var token = data["api-token"]?.Value.ToString();

                        if (token != null)
                        {
                            //save api toke to DAS_Users
                            SqlCommand updateCommand = conn.CreateCommand();
                            string updateSQL = "UPDATE DAS_Users SET apitoken = @apitoken WHERE Email_ID = @email ";

                            updateCommand.CommandText = updateSQL;
                            updateCommand.Parameters.AddWithValue("@apitoken", token);
                            updateCommand.Parameters.AddWithValue("@email", email.GetString());
                            updateCommand.ExecuteNonQuery();

                            //Get Fee Templates
                            url = base_API_Path + "getfeetemplates";

                            var feeModel = new { email = email.GetString(), apiToken = token };

                            response = client.PostAsJsonAsync(url, feeModel).Result;
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                json = response.Content.ReadAsStringAsync().Result;

                                var feeTemplates = JsonConvert.DeserializeObject<FeeTemplates>(json.ToString());

                                toReturn.Add("Fees", feeTemplates);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return toReturn;
        }
    }
}
