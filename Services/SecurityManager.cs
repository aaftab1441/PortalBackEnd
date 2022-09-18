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
    public class SecurityManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public SecurityManager(String connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~SecurityManager()
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
        public Hashtable AuthenticateUser(string EmailId, string Password) { 
            Hashtable toReturn = new Hashtable();

            SqlDataAdapter loginAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM DAS_Users WHERE Email_ID = @EmailID AND Paswd = @Passwd", conn);
            loginAdapter.SelectCommand.Parameters.AddWithValue("@EmailID", EmailId);
            loginAdapter.SelectCommand.Parameters.AddWithValue("@Passwd", Password);

            DataSet users = new DataSet();
            DataSet permissions = new DataSet();

            loginAdapter.Fill(users);
            loginAdapter.Dispose();
            loginAdapter = new SqlDataAdapter("SELECT  * FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @EmailID ", conn);
            loginAdapter.SelectCommand.Parameters.AddWithValue("@EmailID", EmailId);
            loginAdapter.Fill(permissions);

            if(users.Tables[0].Rows.Count == 0)
            {
                loginAdapter = new SqlDataAdapter("SELECT TOP 1 * FROM ITS_merchant WHERE mm_cust_no = @EmailID ", conn);
                loginAdapter.SelectCommand.Parameters.AddWithValue("@EmailID", EmailId);
                loginAdapter.Fill(users);
                toReturn.Add("User", utilityManager.GetDataAsDynamic(users.Tables[0].Rows));

            } else {
                toReturn.Add("User", utilityManager.GetDataAsDynamic(users.Tables[0].Rows));
                toReturn.Add("Permissions", utilityManager.GetDataAsDynamic(permissions.Tables[0].Rows));
            }

            return toReturn;
        }

        public List<Dictionary<string, object>> SearchUsers(JsonElement userObj, JsonElement search) {  
                List<Dictionary<string, object>> toReturn;

            JsonElement permissions = userObj.GetProperty("Permissions");
            string permission = permissions.GetProperty("User_Level_Code").GetString();
            string sql = "", code = "";

            if (permission == "DAS")
            {
                sql = "SELECT* FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID WHERE First_Name like @First_Name AND Last_Name like @Last_Name AND u.Email_ID like @Email_ID  ";
            }
            else if (permission == "ISO")
            {
                sql = "SELECT* FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID WHERE First_Name like @First_Name AND Last_Name like @Last_Name " +
                    " AND u.Email_ID like @Email_ID AND p.iso = @Code  ";
                code = permissions.GetProperty("iso").GetString();
            }
            else if (permission == "SALES-OFFICE")
            {
                sql = "SELECT* FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID WHERE First_Name like @First_Name AND Last_Name like @Last_Name " +
                    " AND u.Email_ID like @Email_ID AND p.sales_office = @Code  ";
                code = permissions.GetProperty("sales_office").GetString();
            }
            else if (permission == "SALES-AGENT")
            {
                sql = "SELECT* FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID WHERE First_Name like @First_Name AND Last_Name like @Last_Name " +
                    " AND u.Email_ID like @Email_ID AND p.sales_agent = @Code  ";
                code = permissions.GetProperty("sales_agent").GetString();
            }
            SqlDataAdapter searchAdapter = new SqlDataAdapter(sql, conn);
            searchAdapter.SelectCommand.Parameters.AddWithValue("@First_Name", String.Format("%{0}%", search.GetProperty("firstName").GetString()));
            searchAdapter.SelectCommand.Parameters.AddWithValue("@Last_Name", String.Format("%{0}%", search.GetProperty("lastName").GetString()));
            searchAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", String.Format("%{0}%", search.GetProperty("emailId").GetString()));
            if(permission != "DAS")
            {
                searchAdapter.SelectCommand.Parameters.AddWithValue("@Code", code);
            }
            //searchAdapter.SelectCommand.Parameters.AddWithValue("@City", String.Format("%{0}%", search.city)); 

            DataSet results = new DataSet();
            searchAdapter.Fill(results); 
            toReturn = utilityManager.GetDataAsDynamic(results.Tables[0].Rows); 
            return toReturn;
        }

        public Hashtable GetUserDetail(JsonElement userObj, JsonElement currentUser)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter permissionAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter merchantUserAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_To_Merchant_Security WHERE Email_ID = @Email_ID  ", conn);
            JsonElement emailId;
            string emailIdStr;

            if(currentUser.TryGetProperty("Email_ID", out emailId))
            {
                emailIdStr = emailId.GetString();
                userAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailIdStr);
                permissionAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailIdStr);
                merchantUserAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailIdStr);
                DataSet userResult = new DataSet();
                DataSet permissionResult = new DataSet();
                DataSet merchantUserResult = new DataSet();

                userAdapter.Fill(userResult);
                permissionAdapter.Fill(permissionResult);
                merchantUserAdapter.Fill(merchantUserResult);

                toReturn.Add("MerchantUser", merchantUserResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(merchantUserResult.Tables[0].Rows[0]) : new ArrayList());
                toReturn.Add("User", userResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(userResult.Tables[0].Rows) : new ArrayList());
                toReturn.Add("Permissions", permissionResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(permissionResult.Tables[0].Rows) : new ArrayList());
                toReturn.Add("UserMerchants", GetUserMerchants(userObj, emailIdStr));
                
            }

            return toReturn;
        }

        public List<Dictionary<string, object>> GetUserMerchants(JsonElement userObj, string emailId)
        {
            List<Dictionary<string, object>> toReturn = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant i INNER JOIN DAS_Users_By_Code_Permissions p ON p.mid = i.mm_cust_no WHERE Email_ID = @Email_ID ", conn);
            JsonElement permissions = userObj.GetProperty("Permissions");
            JsonElement permission;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "ISO")
            {
                searchAdapter = new SqlDataAdapter("SELECT * FROM ITS_Merchant i INNER JOIN DAS_Users_By_Code_Permissions p ON p.mid = i.mm_cust_no WHERE Email_ID = @Email_ID  AND i.iso_code = @isoCode ", conn);
                searchAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", permissions.GetProperty("iso").GetString());
            }

            searchAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailId);

            DataSet results = new DataSet();
            searchAdapter.Fill(results);

            toReturn = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);

            return toReturn;
        }



        public Hashtable GetMyUserDetail(JsonElement userObj, JsonElement currentUser)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter permissionAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter merchantUserAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_To_Merchant_Security WHERE Email_ID = @Email_ID  ", conn);
            JsonElement emailId;
            string email;
            JsonElement userDetails;
            if (currentUser.TryGetProperty("UserDetail", out userDetails) && userDetails.TryGetProperty("Email_ID", out emailId))
            {
                email = emailId.GetString();
                userAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", email);
                permissionAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", email);
                merchantUserAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", email);
                DataSet userResult = new DataSet();
                DataSet permissionResult = new DataSet();
                DataSet merchantUserResult = new DataSet();

                userAdapter.Fill(userResult);
                permissionAdapter.Fill(permissionResult);
                merchantUserAdapter.Fill(merchantUserResult);



                toReturn.Add("MerchantUser", merchantUserResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(merchantUserResult.Tables[0].Rows[0]) : new ArrayList());
                toReturn.Add("User", userResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(userResult.Tables[0].Rows) : new ArrayList());
                toReturn.Add("Permissions", permissionResult.Tables[0].Rows.Count > 0 ? utilityManager.GetDataAsDynamic(permissionResult.Tables[0].Rows) : new ArrayList());
            }


            return toReturn;
        }

        
            public Hashtable DeleteUserAccess(JsonElement userObj, JsonElement currentUser, JsonElement currentMerchant)
        {
            Hashtable toReturn = new Hashtable();
            SqlCommand deleteUser = conn.CreateCommand();
            SqlCommand deletePermission = conn.CreateCommand();
            JsonElement emailId, mid;
            JsonElement userDetails;
            if (currentUser.TryGetProperty("UserDetails", out userDetails) && userDetails.TryGetProperty("Email_ID", out emailId) &&
                currentMerchant.TryGetProperty("mm_cust_no", out mid))
            {
                

                deletePermission.CommandText = "DELETE FROM DAS_Users_By_Code_Permissions WHERE  Email_ID = @Email_ID AND mid = @mid";
                deletePermission.Parameters.AddWithValue("@Email_ID", emailId.GetString());
                deletePermission.Parameters.AddWithValue("@mid", mid.GetString());
                int deletedRows = deletePermission.ExecuteNonQuery();

                toReturn.Add("AffectedRowCount", deletedRows);

            }


            return toReturn;
        }

        public Hashtable EnableUserAccess(JsonElement userObj, JsonElement currentUser, JsonElement currentMerchant)
        {
            Hashtable toReturn = new Hashtable();
            SqlCommand deleteUser = conn.CreateCommand();
            SqlCommand deletePermission = conn.CreateCommand();
            JsonElement emailId, mid;
            JsonElement userDetails;
            SqlCommand permissionsInsert = conn.CreateCommand();
            if (currentUser.TryGetProperty("UserDetails", out userDetails) && userDetails.TryGetProperty("Email_ID", out emailId) &&
                currentMerchant.TryGetProperty("mm_cust_no", out mid))
            {

                permissionsInsert.CommandText = "INSERT INTO DAS_Users_By_Code_Permissions (Email_ID, User_Level_Code, mid) VALUES (@Email_ID, @User_Level_Code, @mid) ";

                permissionsInsert.Parameters.AddWithValue("@Email_ID", emailId.GetString());
                permissionsInsert.Parameters.AddWithValue("@User_Level_Code", "MERCHANT");
                permissionsInsert.Parameters.AddWithValue("@mid", mid.GetString());
                
                int deletedRows = permissionsInsert.ExecuteNonQuery();
                permissionsInsert.Dispose();

                toReturn.Add("AffectedRowCount", deletedRows);

            }


            return toReturn;
        }

        public Hashtable DeleteUser(JsonElement userObj, JsonElement currentUser)
        {
            Hashtable toReturn = new Hashtable();
            SqlCommand deleteUser = conn.CreateCommand();
            SqlCommand deletePermission = conn.CreateCommand();
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter permissionAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter merchantUserAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_To_Merchant_Security WHERE Email_ID = @Email_ID  ", conn);
            JsonElement emailId;
            JsonElement userDetails;
            if (currentUser.TryGetProperty("UserDetails", out userDetails) && userDetails.TryGetProperty("Email_ID", out emailId))
            {
                deleteUser.CommandText = "DELETE FROM Das_Users WHERE  Email_ID = @Email_ID ";
                deleteUser.Parameters.AddWithValue("@Email_ID", emailId.GetString());
                deleteUser.ExecuteNonQuery();

                deletePermission.CommandText = "DELETE FROM DAS_Users_By_Code_Permissions WHERE  Email_ID = @Email_ID ";
                deletePermission.Parameters.AddWithValue("@Email_ID", emailId.GetString());
                deletePermission.ExecuteNonQuery();
                deletePermission.Dispose();


            }

 
            return toReturn;
        }
        public Hashtable SubmitUserDetail(dynamic userObj, JsonElement userContainer)
        {
            dynamic currentUser = userContainer.GetProperty("UserDetails");
            JsonElement permissions = userContainer.GetProperty("Permissions");
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter permissionAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @Email_ID ", conn);
            SqlDataAdapter merchantUserAdapter = new SqlDataAdapter("SELECT *  FROM DAS_Users_To_Merchant_Security WHERE Email_ID = @Email_ID  ", conn);
            SqlCommand userInsertOrUpdateCommand = conn.CreateCommand();
            SqlCommand permissionsInsertOrUpdateCommand = conn.CreateCommand();
            userInsertOrUpdateCommand.CommandText = "UPDATE DAS_Users SET First_Name = @First_Name, Last_Name = @Last_Name, Address = @Address, City = @City,  " +
                     " ST = @ST, Zip = @Zip, Phone = @Phone, User_Level_Code = @User_Level_Code WHERE Email_ID = @Email_ID";
            userAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", currentUser.GetProperty("Email_ID").GetString());
            permissionAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", currentUser.GetProperty("Email_ID").GetString());
            merchantUserAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", currentUser.GetProperty("Email_ID").GetString());
            SqlCommand updatePassword ;
            string password = "", confirmPassword = "";
            JsonElement passwordJson, confirmPasswordJson;
            Hashtable parentTree = getParentObj(permissions);

            string email = currentUser.GetProperty("Email_ID").GetString();
            DataSet userResult = new DataSet();
            DataSet permissionResult = new DataSet();
            DataSet merchantUserResult = new DataSet();
            DataTable userTable;
            DataRow userRow;
            userAdapter.Fill(userResult);
            if(currentUser.TryGetProperty("Passwd", out passwordJson)){
                password = passwordJson.GetString();
            }
            if (currentUser.TryGetProperty("ConfirmPasswd", out confirmPasswordJson))
            {
                confirmPassword = confirmPasswordJson.GetString();
            }

            if (userResult.Tables[0].Rows.Count == 1)
            {//do update
                permissionsInsertOrUpdateCommand.CommandText = "UPDATE DAS_Users_By_Code_Permissions SET iso = @iso, sub_iso = @sub_iso, sales_office = @sales_office, "  +
                     " sales_agent = @sales_agent, iso_id = @iso_id, sub_iso_id = @sub_iso_id, sales_office_id = @sales_office_id, " +
                     " sales_agent_id = @sales_agent_id, User_Level_Code = @User_Level_Code, mid = @mid  WHERE Email_ID = @Email_ID";

                userInsertOrUpdateCommand.Parameters.AddWithValue("@First_Name", utilityManager.TryGetProperty(currentUser, "First_Name"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Last_Name", utilityManager.TryGetProperty(currentUser, "Last_Name"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Address", utilityManager.TryGetProperty(currentUser, "Address"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(currentUser, "City"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@ST", utilityManager.TryGetProperty(currentUser, "ST"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(currentUser, "Zip"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Phone", utilityManager.TryGetProperty(currentUser, "Phone"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@User_Level_Code", utilityManager.TryGetProperty(currentUser, "User_Level_Code"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Email_ID", utilityManager.TryGetProperty(currentUser, "Email_ID"));
                
                userInsertOrUpdateCommand.ExecuteNonQuery();
                userInsertOrUpdateCommand.Dispose();

                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@User_Level_Code", utilityManager.TryGetProperty(currentUser, "User_Level_Code"));
                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@Email_ID", utilityManager.TryGetProperty(currentUser, "Email_ID"));
                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@mid", utilityManager.TryGetProperty(permissions, "mid"));
                foreach (DictionaryEntry k in parentTree)
                {
                    permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@" + k.Key, (string)k.Value);
                }

                permissionsInsertOrUpdateCommand.ExecuteNonQuery();
                permissionsInsertOrUpdateCommand.Dispose();

            }else
            {
                permissionsInsertOrUpdateCommand.CommandText = "INSERT INTO DAS_Users_By_Code_Permissions (iso, sub_iso, sales_office, sales_agent, iso_id, sub_iso_id, sales_office_id, sales_agent_id, Email_ID, User_Level_Code, mid) " +
                    " VALUES (@iso, @sub_iso, @sales_office, @sales_agent, @iso_id, @sub_iso_id, @sales_office_id, @sales_agent_id, @Email_ID, @User_Level_Code, @mid) ";

                userInsertOrUpdateCommand.CommandText = "INSERT INTO DAS_Users (First_Name, Last_Name, Address, City, ST, Zip, Phone, User_Level_Code, Email_ID) " +
                    " VALUES(@First_Name, @Last_Name, @Address, @City,  @ST, @Zip, @Phone, @User_Level_Code, @Email_ID) ";

                userInsertOrUpdateCommand.Parameters.AddWithValue("@First_Name", utilityManager.TryGetProperty(currentUser, "First_Name"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Last_Name", utilityManager.TryGetProperty(currentUser, "Last_Name"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Address", utilityManager.TryGetProperty(currentUser, "Address"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@City", utilityManager.TryGetProperty(currentUser, "City"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@ST", utilityManager.TryGetProperty(currentUser, "ST"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Zip", utilityManager.TryGetProperty(currentUser, "Zip"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Phone", utilityManager.TryGetProperty(currentUser, "Phone"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@User_Level_Code", utilityManager.TryGetProperty(currentUser, "User_Level_Code"));
                userInsertOrUpdateCommand.Parameters.AddWithValue("@Email_ID", utilityManager.TryGetProperty(currentUser, "Email_ID"));

                userInsertOrUpdateCommand.ExecuteNonQuery();
                userInsertOrUpdateCommand.Dispose();

                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@User_Level_Code", utilityManager.TryGetProperty(currentUser, "User_Level_Code"));
                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@Email_ID", utilityManager.TryGetProperty(currentUser, "Email_ID"));
                permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@mid", utilityManager.TryGetProperty(permissions, "mid"));

                foreach(DictionaryEntry k in parentTree)
                {
                    permissionsInsertOrUpdateCommand.Parameters.AddWithValue("@" + k.Key, (string) k.Value);
                }


                permissionsInsertOrUpdateCommand.ExecuteNonQuery();
                permissionsInsertOrUpdateCommand.Dispose();
            }


            if (password == confirmPassword && password.Length > 1)
            {
                updatePassword = conn.CreateCommand();
                updatePassword.CommandText = "UPDATE DAS_Users SET Paswd = @Paswd  WHERE Email_ID = @Email_ID";
                updatePassword.Parameters.AddWithValue("@Paswd", password);
                updatePassword.Parameters.AddWithValue("@Email_ID", currentUser.GetProperty("Email_ID").GetString());
                updatePassword.ExecuteNonQuery();
                updatePassword.Dispose();
            }
            return toReturn;
        }

        public string getValueFromDb(string table, string selectField, string field, string filterValue)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT " + selectField + "  FROM " + table + " WHERE " + field + " = @" + field , conn);
            DataSet result = new DataSet();
            dataAdapter.SelectCommand.Parameters.AddWithValue("@" + field, filterValue);
            dataAdapter.Fill(result);
            string toReturn = (string) utilityManager.GetFirstValue(result.Tables[0].Rows, selectField);
            
            return toReturn;

        }
        public Dictionary<string, object> getObjectFromDb(string table, string field, string filterValue)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT *  FROM " + table + " WHERE " + field + " = @" + field, conn);
            DataSet result = new DataSet();
            dataAdapter.SelectCommand.Parameters.AddWithValue("@" + field, filterValue);
            dataAdapter.Fill(result);
            Dictionary<string, object> toReturn = null;
            if (result.Tables[0].Rows.Count > 0)
            {
                toReturn = utilityManager.GetFirstRowDataAsDynamic(result.Tables[0].Rows[0]);
            }

            return toReturn;

        }

        private Hashtable getParentHierarchy(Dictionary<string, object> data, string currentType)
        {
            Hashtable toReturn = new Hashtable();
            Hashtable parentHierarchy = new Hashtable();
            parentHierarchy.Add("SALES-AGENT", new OrganizationHierarchy("SALES-AGENT", "Sales_Agent", "sales_agent", "sales_agent_id", "Code", "SALES-OFFICE"));
            parentHierarchy.Add("SALES-OFFICE", new OrganizationHierarchy("SALES-OFFICE", "Sales_Office", "sales_office", "sales_office_id", "Code", "SUB-ISO"));
            parentHierarchy.Add("SUB-ISO", new OrganizationHierarchy("SUB-ISO", "Sub_Iso",  "sub_iso", "sub_iso_id", "Code", "ISO"));
            parentHierarchy.Add("ISO", new OrganizationHierarchy("ISO_MASTER", "ISO_MASTER", "iso", "iso_id", "ISO_CODE", null));
            parentHierarchy.Add("DAS", new OrganizationHierarchy("ISO_MASTER", "ISO_MASTER", "iso", "iso_id", "ISO_CODE", null));
            string[] parentList =  new string []{ "SALES-AGENT", "SALES-OFFICE", "SUB-ISO", "ISO" };

            OrganizationHierarchy currentOrg = (OrganizationHierarchy)parentHierarchy[currentType];

            toReturn.Add(currentOrg.parentField, data[currentOrg.tableCode]);
            toReturn.Add(currentOrg.parentField + "_id", data["AutoIdent"]);
            if (!data.ContainsKey("Parent_Type"))
            {//we are at the iso level there are no more levels down
                return toReturn;
            }
            string parentType = (string) data["Parent_Type"];
            string parentId = (string) data["Parent_Id"];
            string parentCode = (string)data["Parent_Code"];
            string tableFieldCode = ((OrganizationHierarchy) parentHierarchy[parentType]).parentField;

            toReturn.Add(tableFieldCode, parentCode);
            toReturn.Add(tableFieldCode + "_id", parentId);

            string currentParentType = parentType;

            OrganizationHierarchy org;
            Dictionary<string, object> row;

            for (int i = 0; i < parentList.Length; i++)
            {
                if(parentList[i] == parentType)
                {
                    org = (OrganizationHierarchy) parentHierarchy[parentType];
                    tableFieldCode = org.tableCode;
                    row = getObjectFromDb(org.parentTable, "AutoIdent", parentId);
                    if (!toReturn.ContainsKey(org.parentField))
                    {
                        toReturn.Add(org.parentField, row[org.tableCode]);
                        toReturn.Add(org.parentField + "_id", row["AutoIdent"]);
                    }

                    parentType = org.nextParent;
                    if (row.ContainsKey("Parent_Id"))
                    {
                        parentId = (string)row["Parent_Id"];
                    }else
                    {
                        break;
                    }
                    
                    if(parentType == null)
                    {
                        break;
                    }
                }
            }
            
            return toReturn;
        }
        private Hashtable getParentObj(JsonElement permissions)
        {
            Hashtable toReturn = new Hashtable(), parentTree = new Hashtable();
            if(permissions.ValueKind == JsonValueKind.Array)
            {
                permissions = permissions[0];
            }
            string isoId = utilityManager.TryGetProperty(permissions, "iso_id");
            string subIsoId = utilityManager.TryGetProperty(permissions, "sub_iso_id");
            string salesOfficeId = utilityManager.TryGetProperty(permissions, "sales_office_id");
            string salesRepId = utilityManager.TryGetProperty(permissions, "sales_agent_id");
            string[] fields = new string[] { "iso", "sub_iso", "sales_office", "sales_agent", "iso_id", "sub_iso_id", "sales_office_id", "sales_agent_id" };
            
            List<Dictionary<string, object>> dataObj; 

            Dictionary<string, object> row  = new Dictionary<string, object>();
            string currentType = "";
            if (isoId != null && isoId.Length > 0)
            {
                row = getObjectFromDb("ISO_MASTER", "AutoIdent", isoId);
                currentType = "ISO";
            }

            if (subIsoId != null && subIsoId.Length > 0)
            {
                row = getObjectFromDb("Sub_Iso", "AutoIdent", subIsoId);
                currentType = "SUB-ISO";
            }

            if (salesOfficeId != null && salesOfficeId.Length > 0)
            {
                row = getObjectFromDb("Sales_Office", "AutoIdent", salesOfficeId);
                currentType = "SALES-OFFICE";
            }

            if (salesRepId != null && salesRepId.Length > 0)
            {
                row = getObjectFromDb("Sales_Agent", "AutoIdent", salesRepId);
                currentType = "SALES-AGENT";
            }

            parentTree = getParentHierarchy(row, currentType);
            
            
            for(int i = 0; i < fields.Length; i++)
            {
                toReturn.Add(fields[i], "");
            }
            foreach(DictionaryEntry k in parentTree)
            {
                if (toReturn.Contains(k.Key))
                {
                    toReturn.Remove(k.Key);
                    toReturn.Add(k.Key, k.Value);
                }
            }
            
            return toReturn;
        }
    }
}
