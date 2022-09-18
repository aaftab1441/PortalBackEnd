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
    public class SalesOfficeManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public SalesOfficeManager(String connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~SalesOfficeManager()
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
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Office WHERE AutoIdent = @id  ", conn);
            SqlDataAdapter salesAgentAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Agent WHERE Parent_Id = @id AND Parent_Type = @parentType ", conn);
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT u.*, p.sub_iso FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID  WHERE sales_office_id = @id   AND u.User_Level_Code = @userLevel  ", conn);
            JsonElement permissions = userObj.GetProperty("Permissions");
            DataSet results = new DataSet(), users = new DataSet(), salesAgents = new DataSet();
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

                userAdapter.SelectCommand.Parameters.AddWithValue("@id", id);
                userAdapter.SelectCommand.Parameters.AddWithValue("@userLevel", "SALES-OFFICE");
                results = new DataSet();
                userAdapter.Fill(results);

                returnResults = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);
                toReturn.Add("Users", returnResults);


                salesAgentAdapter.SelectCommand.Parameters.AddWithValue("@id", id);
                salesAgentAdapter.SelectCommand.Parameters.AddWithValue("@parentType", "SALES-OFFICE");
                salesAgentAdapter.Fill(salesAgents);
                toReturn.Add("SalesAgents", utilityManager.GetDataAsDynamic(salesAgents.Tables[0].Rows));

            }


            return toReturn;
        }

        public List<Dictionary<string, object>> List(JsonElement userObj, JsonElement search)
        {

            List<Dictionary<string, object>> returnResults = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Office WHERE Name like @name AND Street_Address like @streetAddress AND City like @city " +
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
                deleteCommand.CommandText = "DELETE FROM Sales_Office WHERE AutoIdent = @AutoIdent ";
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
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM Sales_Office WHERE Code = @Code  ", conn);
            SqlCommand addOrUpdateCommand = conn.CreateCommand();
            string insertSQL = "INSERT INTO Sales_Office  (Code, Name, Street_Address, City, State, ZIP, Contact_First_Name, Contact_Last_Name, " +
                " Contact_Email_ID, Contact_Main_Phone, Contact_Cell_Phone, Active_status_code, DateTime_Added, DateTime_Updated, Parent_Type, Parent_Code, Parent_Id) VALUES (@Code, @Name,  @StreetAddress, @City, @State, @Zip, " +
                " @contactFirstName, @contactLastName, @contactEmailID, @contactMainPhone, @contactCellPhone, @activeStatus, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, @parentType, @parentCode, @parentId) ";

            string updateSQL = "UPDATE Sales_Office SET Name = @Name, Street_Address = @StreetAddress, City = @City, State = @State, " +
                " ZIP = @Zip, Contact_First_Name = @contactFirstName, Contact_Last_Name = @contactLastName, Contact_Email_ID = @contactEmailID, Contact_Main_Phone = @contactMainPhone, " +
                " Contact_Cell_Phone = @contactCellPhone, Active_status_code = @activeStatus, DateTime_Updated = CURRENT_TIMESTAMP WHERE AutoIdent = @id ";
            int affectedRows = 0;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "DAS")
            {
                string id = utilityManager.TryGetProperty(detail, "AutoIdent");
                if (id != "")
                {
                    addOrUpdateCommand.CommandText = updateSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@id", id);
                    addOrUpdateCommand.Parameters.AddWithValue("@Name", utilityManager.TryGetProperty(detail, "Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@StreetAddress", utilityManager.TryGetProperty(detail, "Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(detail, "City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@State", utilityManager.TryGetProperty(detail, "State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(detail, "ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactFirstName", utilityManager.TryGetProperty(detail, "Contact_First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactLastName", utilityManager.TryGetProperty(detail, "Contact_Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactEmailID", utilityManager.TryGetProperty(detail, "Contact_Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactMainPhone", utilityManager.TryGetProperty(detail, "Contact_Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactCellPhone", utilityManager.TryGetProperty(detail, "Contact_Cell_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@activeStatus", utilityManager.TryGetProperty(detail, "Active_status_code"));

                    affectedRows = addOrUpdateCommand.ExecuteNonQuery();

                }
                else
                {
                    addOrUpdateCommand.CommandText = insertSQL;
                    addOrUpdateCommand.Parameters.AddWithValue("@Code", utilityManager.TryGetProperty(detail, "Code"));
                    addOrUpdateCommand.Parameters.AddWithValue("@Name", utilityManager.TryGetProperty(detail, "Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@StreetAddress", utilityManager.TryGetProperty(detail, "Street_Address"));
                    addOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(detail, "City"));
                    addOrUpdateCommand.Parameters.AddWithValue("@State", utilityManager.TryGetProperty(detail, "State"));
                    addOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(detail, "ZIP"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactFirstName", utilityManager.TryGetProperty(detail, "Contact_First_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactLastName", utilityManager.TryGetProperty(detail, "Contact_Last_Name"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactEmailID", utilityManager.TryGetProperty(detail, "Contact_Email_ID"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactMainPhone", utilityManager.TryGetProperty(detail, "Contact_Main_Phone"));
                    addOrUpdateCommand.Parameters.AddWithValue("@contactCellPhone", utilityManager.TryGetProperty(detail, "Contact_Cell_Phone"));
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
