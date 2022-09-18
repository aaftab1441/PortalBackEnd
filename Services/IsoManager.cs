using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;
using System.Text.Json;

namespace EaglePortal.Services
{
    public class IsoManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public IsoManager(String connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~IsoManager()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }catch(Exception e)
            {

            }
        }

        public Dictionary<string, object> Get(JsonElement userObj, JsonElement iso) {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            List<Dictionary<string, object>> returnResults = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT * FROM ISO_MASTER WHERE AutoIdent = @id  ", conn);
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT u.*, p.iso   FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID  WHERE iso = @isoCode AND u.User_Level_Code = @userLevel ", conn);
            SqlDataAdapter subIsoAdapter = new SqlDataAdapter("SELECT *  FROM Sub_Iso WHERE Parent_Code = @isoCode AND Parent_Type = @parentType ", conn);
            SqlDataAdapter salesOfficeAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Office WHERE Parent_Code = @isoCode AND Parent_Type = @parentType ", conn);
            SqlDataAdapter salesAgentAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Agent WHERE Parent_Code = @isoCode AND Parent_Type = @parentType ", conn);
            JsonElement permissions = userObj.GetProperty("Permissions");
            DataSet results = new DataSet(), users = new DataSet(), subIsos = new DataSet(), salesOffices = new DataSet(), salesAgents = new DataSet();
            JsonElement isoId;
            string id = "";
            Dictionary<string, object> itemRow;
            //iso.GetProperty("User_Level_Code", out isoId);
            JsonElement isoDetail;
            if (iso.TryGetProperty("IsoDetail", out isoDetail))
            {
                id = isoDetail.GetProperty("AutoIdent").GetString();
            }else
            {
                id = iso.GetProperty("AutoIdent").GetString();
            }
            JsonElement permission;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                searchAdapter.SelectCommand.Parameters.AddWithValue("@id", id);

                
                searchAdapter.Fill(results);

                returnResults = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);
                itemRow = returnResults[0];
                toReturn.Add("IsoDetail", itemRow);

                userAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", itemRow["ISO_CODE"]);
                userAdapter.SelectCommand.Parameters.AddWithValue("@userLevel", "ISO");
                
                userAdapter.Fill(users);
                toReturn.Add("Users", utilityManager.GetDataAsDynamic(users.Tables[0].Rows));

                subIsoAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", itemRow["ISO_CODE"]);
                subIsoAdapter.SelectCommand.Parameters.AddWithValue("@parentType", "ISO");
                subIsoAdapter.Fill(subIsos);
                toReturn.Add("SubIsos", utilityManager.GetDataAsDynamic(subIsos.Tables[0].Rows));

                salesOfficeAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", itemRow["ISO_CODE"]);
                salesOfficeAdapter.SelectCommand.Parameters.AddWithValue("@parentType", "ISO");
                salesOfficeAdapter.Fill(salesOffices);
                toReturn.Add("SalesOffices", utilityManager.GetDataAsDynamic(salesOffices.Tables[0].Rows));

                salesAgentAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", itemRow["ISO_CODE"]);
                salesAgentAdapter.SelectCommand.Parameters.AddWithValue("@parentType", "ISO");
                salesAgentAdapter.Fill(salesAgents);
                toReturn.Add("SalesAgents", utilityManager.GetDataAsDynamic(salesAgents.Tables[0].Rows));

            }


            return toReturn;
        }

        public List<Dictionary<string, object>> List(JsonElement userObj, JsonElement search)
        {
            
            List<Dictionary<string, object>> returnResults  = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM ISO_MASTER WHERE ISO_NAME like @name AND ISO_Street_Address like @streetAddress AND ISO_City like @city " +
           " AND ISO_State  like @state ORDER BY ISO_CODE ", conn);
            JsonElement permissions , permission;
            if (userObj.TryGetProperty("Permissions", out permissions) && permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                searchAdapter.SelectCommand.Parameters.AddWithValue("@name", string.Format("%{0}%", search.GetProperty("name").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@streetAddress", string.Format("%{0}%", search.GetProperty("streetAddress").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@city", string.Format("%{0}%", search.GetProperty("city").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@state", string.Format("%{0}%", search.GetProperty("state").GetString()));


                DataSet results = new DataSet();
                searchAdapter.Fill(results);

                returnResults = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);
            }

             
            return returnResults;
        }
        public Dictionary<string, object> Delete(JsonElement userObj, JsonElement iso)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand deleteCommand = conn.CreateCommand();
            JsonElement permissions = userObj.GetProperty("Permissions");
            JsonElement permission;
            JsonElement isoDetail = iso.GetProperty("IsoDetail");
            int affectedRows = 0;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                deleteCommand.CommandText = "DELETE FROM ISO_MASTER WHERE AutoIdent = @AutoIdent ";
                deleteCommand.Parameters.AddWithValue("@AutoIdent", utilityManager.TryGetProperty(isoDetail, "AutoIdent"));
                affectedRows = deleteCommand.ExecuteNonQuery();
            }
            toReturn.Add("Success", affectedRows > 0? true: false);
            return toReturn;
        }

        public Dictionary<string, object> Save(JsonElement userObj, JsonElement iso)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            JsonElement permissions = userObj.GetProperty("Permissions");
            JsonElement permission;

            JsonElement isoDetail = iso.GetProperty("IsoDetail");
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM ISO_MASTER WHERE ISO_CODE = @isoCode  ", conn);
            SqlCommand addOrUpdateCommand = conn.CreateCommand();
            string insertSQL = "INSERT INTO ISO_MASTER  (ISO_CODE, ISO_NAME, ISO_Street_Address, ISO_City, ISO_State, ISO_ZIP, Contact_First_Name, Contact_Last_Name, " +
                " Contact_Email_ID, Contact_Main_Phone, Contact_Cell_Phone, Active_status_code, DateTime_Added, DateTime_Updated) VALUES (@isoCode, @isoName,  @isoStreetAddress, @isoCity, @isoState, @isoZip, " +
                " @contactFirstName, @contactLastName, @contactEmailID, @contactMainPhone, @contactCellPhone, @activeStatus, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP) ";

            string updateSQL = "UPDATE ISO_MASTER SET ISO_NAME = @isoName, ISO_Street_Address = @isoStreetAddress, ISO_City = @isoCity, ISO_State = @isoState, " +
                " ISO_ZIP = @isoZip, Contact_First_Name = @contactFirstName, Contact_Last_Name = @contactLastName, Contact_Email_ID = @contactEmailID, Contact_Main_Phone = @contactMainPhone, " +
                " Contact_Cell_Phone = @contactCellPhone, Active_status_code = @activeStatus, DateTime_Updated = CURRENT_TIMESTAMP WHERE ISO_CODE = @isoCode ";
            int affectedRows = 0;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                searchAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", utilityManager.TryGetProperty(isoDetail, "ISO_CODE"));
                DataSet results = new DataSet();
                searchAdapter.Fill(results);
                if (utilityManager.TryGetProperty(isoDetail, "AutoIdent") != "")
                {
                    addOrUpdateCommand.CommandText = updateSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@isoCode", utilityManager.TryGetProperty(isoDetail, "ISO_CODE"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoName", utilityManager.TryGetProperty(isoDetail, "ISO_NAME"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoStreetAddress", utilityManager.TryGetProperty(isoDetail, "ISO_Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoCity", utilityManager.TryGetProperty(isoDetail, "ISO_City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoState", utilityManager.TryGetProperty(isoDetail, "ISO_State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoZip", utilityManager.TryGetProperty(isoDetail, "ISO_ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactFirstName", utilityManager.TryGetProperty(isoDetail, "Contact_First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactLastName", utilityManager.TryGetProperty(isoDetail, "Contact_Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactEmailID", utilityManager.TryGetProperty(isoDetail, "Contact_Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactMainPhone", utilityManager.TryGetProperty(isoDetail, "Contact_Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactCellPhone", utilityManager.TryGetProperty(isoDetail, "Contact_Cell_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@activeStatus", utilityManager.TryGetProperty(isoDetail, "Active_status_code"));

                    affectedRows = addOrUpdateCommand.ExecuteNonQuery();

                }
                else
                {
                    addOrUpdateCommand.CommandText = insertSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@isoCode", utilityManager.TryGetProperty(isoDetail, "ISO_CODE"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoName", utilityManager.TryGetProperty(isoDetail, "ISO_NAME"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoStreetAddress", utilityManager.TryGetProperty(isoDetail, "ISO_Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoCity", utilityManager.TryGetProperty(isoDetail, "ISO_City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoState", utilityManager.TryGetProperty(isoDetail, "ISO_State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@isoZip", utilityManager.TryGetProperty(isoDetail, "ISO_ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactFirstName", utilityManager.TryGetProperty(isoDetail, "Contact_First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactLastName", utilityManager.TryGetProperty(isoDetail, "Contact_Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactEmailID", utilityManager.TryGetProperty(isoDetail, "Contact_Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactMainPhone", utilityManager.TryGetProperty(isoDetail, "Contact_Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactCellPhone", utilityManager.TryGetProperty(isoDetail, "Contact_Cell_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@activeStatus", utilityManager.TryGetProperty(isoDetail, "Active_status_code"));
                    affectedRows = addOrUpdateCommand.ExecuteNonQuery();
                }
                 
                 
            }
            toReturn.Add("Success", affectedRows > 0 ? true : false);
            return toReturn;
        }


    }
}
