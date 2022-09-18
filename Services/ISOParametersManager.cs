using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;
using System.Text.Json;
using EaglePortal.Models;

namespace EaglePortal.Services
{
    public class ISOParametersManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public ISOParametersManager(string connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~ISOParametersManager()
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

        public List<Dictionary<string, object>> SearchISOParameters(JsonElement userObj, JsonElement search)
        {
            List<Dictionary<string, object>> toReturn = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM ISO_Parameters", conn);
            
            DataSet results = new DataSet();
            searchAdapter.Fill(results);

            toReturn = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);

            return toReturn;
        }

        public Hashtable GetPageData(JsonElement userObj, JsonElement parameterContainer, JsonElement pageInfo)
        {
            Hashtable pageItems = utilityManager.GetHashMap(pageInfo);
            JsonElement isoParameter = parameterContainer.GetProperty("IsoParameter");
            string code = isoParameter.GetProperty("ISO_Code").GetString();
            return GetPageData(userObj, code, pageItems);
        }

        public Hashtable GetPageData(JsonElement userObj, string code, Hashtable pageInfo)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter parameterAdapter = new SqlDataAdapter("SELECT *  FROM ISO_Parameters WHERE ISO_Code = @ISO_Code ", conn);

            string search = "";
            Hashtable searchData = new Hashtable();
            if (pageInfo.ContainsKey("Search") && pageInfo["Search"] != null && pageInfo["Search"] is string)
            {
                search = (string)pageInfo["Search"];
                searchData = JsonSerializer.Deserialize<Hashtable>(search);
            }


            string tableName = (string)pageInfo["Name"];
            string sortField = (string)pageInfo["SortField"];
            string sortDirection = (string)pageInfo["SortDirection"];
            int page = Int32.Parse((string)pageInfo["Page"]) - 1;

            if (page < 0)
            {
                page = 0;
            }
            int pageSize = Int32.Parse((string)pageInfo["PageSize"]);
            CriteriaCollection criteriaCollection = new CriteriaCollection();

            
            DataSet result = new DataSet();
            DataSet countResult = new DataSet();

            parameterAdapter.Fill(countResult);

            toReturn.Add("SortDirection", sortDirection);
            toReturn.Add("Name", tableName);
            toReturn.Add("SortField", sortField);
            toReturn.Add("Page", page);
            toReturn.Add("PageSize", pageSize);
            toReturn.Add("Data", utilityManager.GetDataAsDynamic(result.Tables[0].Rows));
            toReturn.Add("Count", utilityManager.GetFirstValue(countResult.Tables[0].Rows, "Count"));

            return toReturn;
        }

        public Hashtable GetISOParametersDetail(JsonElement userObj, JsonElement parameterContainer)
        {
            Hashtable toReturn = new Hashtable();
            string ISO_Code = "";
            JsonElement permissions;
            JsonElement parameter;
            JsonElement code;
            DataSet parametersResult = new DataSet();
            DataSet userResults = new DataSet();
            if (userObj.TryGetProperty("Permissions", out permissions))
            {
                if (parameterContainer.TryGetProperty("IsoParameter", out parameter) && parameter.TryGetProperty("ISO_Code", out code))
                {
                    ISO_Code = code.GetString();
                }

                if (ISO_Code != null && ISO_Code.Length > 0)
                {
                    using (SqlDataAdapter parameterAdapter = new SqlDataAdapter("SELECT *  FROM ISO_Parameters WHERE ISO_Code = @ISO_Code ", conn))
                    {
                        parameterAdapter.SelectCommand.Parameters.AddWithValue("@CustNo", ISO_Code);
                        parameterAdapter.SelectCommand.CommandTimeout = 0;
                        parameterAdapter.Fill(parametersResult);
                    }

                    List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
                    toReturn.Add("Users", utilityManager.GetDataAsDynamic(userResults.Tables[0].Rows));
                    toReturn.Add("ISO_Parameters", utilityManager.GetDataAsDynamic(parametersResult));
                }
            }

            return toReturn;
        }
    }
}
