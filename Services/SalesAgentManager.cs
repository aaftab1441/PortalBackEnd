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
    public class SalesAgentManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public SalesAgentManager(String connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~SalesAgentManager()
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
        public Dictionary<string, object> Get(JsonElement userObj, JsonElement iso)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            List<Dictionary<string, object>> returnResults = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Agent WHERE AutoIdent = @id  ", conn);
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT u.*, p.sales_agent FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID  WHERE sales_agent_id = @id   AND u.User_Level_Code = @userLevel ", conn);
            JsonElement permissions = userObj.GetProperty("Permissions");
            DataSet results = new DataSet(), users = new DataSet();
            JsonElement isoId;
            string id = "";
            Dictionary<string, object> itemRow;
            //iso.GetProperty("User_Level_Code", out isoId);
            JsonElement isoDetail;
            if (iso.TryGetProperty("Detail", out isoDetail))
            {
                id = isoDetail.GetProperty("AutoIdent").GetString();
            }
            else
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
                toReturn.Add("Detail", itemRow);

                userAdapter.SelectCommand.Parameters.AddWithValue("@id", itemRow["AutoIdent"]);
                userAdapter.SelectCommand.Parameters.AddWithValue("@userLevel", "SALES-AGENT");
                results = new DataSet();
                userAdapter.Fill(results);

                returnResults = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);
                toReturn.Add("Users", returnResults);

            }


            return toReturn;
        }

        public List<Dictionary<string, object>> List(JsonElement userObj, JsonElement search)
        {

            List<Dictionary<string, object>> returnResults = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Agent WHERE First_Name like @name AND Street_Address like @streetAddress AND City like @city " +
           " AND State like @state AND Parent_Type = @parentType AND Parent_Id = @parentId ORDER BY Code ", conn);
            JsonElement permissions;
            string permission, parentId;
            if (userObj.TryGetProperty("Permissions", out permissions))
            {
                permission = utilityManager.TryGetProperty(permissions, "User_Level_Code");
                parentId = utilityManager.TryGetProperty(permissions, utilityManager.getParentIdField(permission));

                searchAdapter.SelectCommand.Parameters.AddWithValue("@name", string.Format("%{0}%", search.GetProperty("name").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@streetAddress", string.Format("%{0}%", search.GetProperty("streetAddress").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@city", string.Format("%{0}%", search.GetProperty("city").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@state", string.Format("%{0}%", search.GetProperty("state").GetString()));
                searchAdapter.SelectCommand.Parameters.AddWithValue("@parentType", permission);
                searchAdapter.SelectCommand.Parameters.AddWithValue("@parentId", parentId);


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
            JsonElement isoDetail = iso.GetProperty("Detail");
            int affectedRows = 0;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                deleteCommand.CommandText = "DELETE FROM Sales_Agent WHERE AutoIdent = @AutoIdent ";
                deleteCommand.Parameters.AddWithValue("@AutoIdent", utilityManager.TryGetProperty(isoDetail, "AutoIdent"));
                affectedRows = deleteCommand.ExecuteNonQuery();
            }
            toReturn.Add("Success", affectedRows > 0 ? true : false);
            return toReturn;
        }

        public Dictionary<string, object> Save(JsonElement userObj, JsonElement iso)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            JsonElement permissions = userObj.GetProperty("Permissions");
            JsonElement permission;

            JsonElement detail = iso.GetProperty("Detail");
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Agent WHERE Code = @Code  ", conn);
            SqlCommand addOrUpdateCommand = conn.CreateCommand();
            string insertSQL = "INSERT INTO Sales_Agent  (Code, Middle_Name, Street_Address, City, State, ZIP, First_Name, Last_Name, " +
                " Email_ID, Main_Phone, Cell_Phone, Active_status_code, DateTime_Added, DateTime_Updated, Parent_Type, Parent_Code, Parent_Id) VALUES (@Code, @middleName,  @StreetAddress, @City, @State, @Zip, " +
                " @firstName, @lastName, @EmailID, @MainPhone, @CellPhone, @activeStatus, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @parentType, @parentCode, @parentId) ";

            string updateSQL = "UPDATE Sales_Agent SET Middle_Name = @middleName, Street_Address = @StreetAddress, City = @City, State = @State, " +
                " ZIP = @Zip, First_Name = @firstName, Last_Name = @lastName, Email_ID = @EmailID, Main_Phone = @MainPhone, " +
                " Cell_Phone = @CellPhone, Active_status_code = @activeStatus, DateTime_Updated = CURRENT_TIMESTAMP, Code = @Code WHERE AutoIdent = @id ";
            int affectedRows = 0;
            string id = utilityManager.TryGetProperty(detail, "AutoIdent");
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                if (id.Length > 0)
                {
                    addOrUpdateCommand.CommandText = updateSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@id", id);
                    addOrUpdateCommand.Parameters.AddWithValue("@Code", utilityManager.TryGetProperty(detail, "Code"));
                    addOrUpdateCommand.Parameters.AddWithValue("@middleName", utilityManager.TryGetProperty(detail, "Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@StreetAddress", utilityManager.TryGetProperty(detail, "Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(detail, "City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@State", utilityManager.TryGetProperty(detail, "State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(detail, "ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@firstName", utilityManager.TryGetProperty(detail, "First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@lastName", utilityManager.TryGetProperty(detail, "Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@EmailID", utilityManager.TryGetProperty(detail, "Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@MainPhone", utilityManager.TryGetProperty(detail, "Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@CellPhone", utilityManager.TryGetProperty(detail, "Cell_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@activeStatus", utilityManager.TryGetProperty(detail, "Active_status_code"));

                    affectedRows = addOrUpdateCommand.ExecuteNonQuery();

                }
                else
                {
                    addOrUpdateCommand.CommandText = insertSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@Code", utilityManager.TryGetProperty(detail, "Code"));
                    addOrUpdateCommand.Parameters.AddWithValue("@middleName", utilityManager.TryGetProperty(detail, "Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@StreetAddress", utilityManager.TryGetProperty(detail, "Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(detail, "City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@State", utilityManager.TryGetProperty(detail, "State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(detail, "ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@firstName", utilityManager.TryGetProperty(detail, "First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@lastName", utilityManager.TryGetProperty(detail, "Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@EmailID", utilityManager.TryGetProperty(detail, "Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@MainPhone", utilityManager.TryGetProperty(detail, "Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@CellPhone", utilityManager.TryGetProperty(detail, "Cell_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@activeStatus", utilityManager.TryGetProperty(detail, "Active_status_code"));
                    addOrUpdateCommand.Parameters.AddWithValue("@parentType", utilityManager.TryGetProperty(detail, "Parent_Type"));
                    addOrUpdateCommand.Parameters.AddWithValue("@parentCode", utilityManager.TryGetProperty(detail, "Parent_Code"));
                    addOrUpdateCommand.Parameters.AddWithValue("@parentId", utilityManager.TryGetProperty(detail, "Parent_Id"));

                    affectedRows = addOrUpdateCommand.ExecuteNonQuery();
                }


            }
            toReturn.Add("Success", affectedRows > 0 ? true : false);
            return toReturn;
        }


    }

}
