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
    public class MerchantSearchManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public MerchantSearchManager(string connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~MerchantSearchManager()
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

        public Hashtable GetMerchantDashboardData(string merchantNumber)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter dashboardAdapter, firstAccountAdapter;
            JsonElement userDetails, email = new JsonElement();
             
            DataSet dashboardData = new DataSet(), firstAccountData = new DataSet();
            

            string dashboardQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode ORDER BY category, variable_name, period ";
            if (merchantNumber != null && merchantNumber.Length > 0)
            {
                dashboardAdapter = new SqlDataAdapter(dashboardQuery, conn);
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "MCT");
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", merchantNumber);
                dashboardAdapter.Fill(dashboardData);
                dashboardAdapter.Dispose();

            }
            
             

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData = utilityManager.CategorizeData(allData);
            // utilityManager.GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost");

            toReturn.Add("CurrentMid", merchantNumber);

            toReturn.Add("CurrentYTDSales",  utilityManager.GetValue(categorizedData, "Sales", "Current_YTD"));
            toReturn.Add("PreviousYTDSales",  utilityManager.GetValue(categorizedData, "Sales", "Previous_YTD"));

            toReturn.Add("CurrentYTD",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "Sales", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTD",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "Sales", "Last_Year_Past_Six_Months"), true));

            toReturn.Add("TransactionCount",  utilityManager.GetValue(categorizedData, "Transaction_Counts", "Current_Month"));
            toReturn.Add("ChargeBackCount",  utilityManager.GetValue(categorizedData, "ChargeBacks", "Current_ChargeBacks"));

            toReturn.Add("PreviousYTDChargeBacks",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "ChargeBacks", "Previous_ChargeBacks_Chart"), true));
            toReturn.Add("CurrentYTDChargeBacks",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "ChargeBacks", "Current_ChargeBacks_Chart"), false));

            toReturn.Add("CurrentYTDTransactions",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "Transaction_Counts", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTDTransactions",  utilityManager.processDateConversion( utilityManager.GetChartData(categorizedData, "Transaction_Counts", "Last_Year_Past_Six_Months"), true));
            toReturn.Add("Risk", utilityManager.GetRiskData(categorizedData));
            
            return toReturn;
        }
        public List<Dictionary<string, object>> SearchMerchants(JsonElement userObj, JsonElement search)
        {
            List<Dictionary<string, object>> toReturn = new List<Dictionary<string, object>>();
            SqlDataAdapter searchAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant WHERE mm_legal_name like @legalName AND mm_dba_name like @dbaName AND mm_cust_no like @mid  " +
            " AND mm_owner_last_1  like @ownerLastName ", conn);
            JsonElement permissions = userObj.GetProperty("Permissions");
            JsonElement permission;
            if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "ISO")
            {
                searchAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant WHERE mm_legal_name like @legalName AND mm_dba_name like @dbaName AND mm_cust_no like @mid  " +
                    " AND mm_owner_last_1  like @ownerLastName AND iso_code = @isoCode ", conn);
                searchAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", permissions.GetProperty("iso").GetString());
            }

            searchAdapter.SelectCommand.Parameters.AddWithValue("@legalName", string.Format("%{0}%", utilityManager.TryGetProperty(search, "legalName")));
            searchAdapter.SelectCommand.Parameters.AddWithValue("@dbaName", string.Format("%{0}%", utilityManager.TryGetProperty(search, "dbaName")));
            searchAdapter.SelectCommand.Parameters.AddWithValue("@mid", string.Format("%{0}%", utilityManager.TryGetProperty(search, "mid")));
            searchAdapter.SelectCommand.Parameters.AddWithValue("@ownerLastName", string.Format("%{0}%", utilityManager.TryGetProperty(search, "ownerLastName")));


            DataSet results = new DataSet();
            searchAdapter.Fill(results);

            toReturn = utilityManager.GetDataAsDynamic(results.Tables[0].Rows);

            return toReturn;
        }

        public Hashtable GetPageData(JsonElement userObj, JsonElement merchantContainer, JsonElement pageInfo)
        {
            Hashtable pageItems = utilityManager.GetHashMap(pageInfo);
            JsonElement merchant = merchantContainer.GetProperty("Merchant");
            string merchantNo = merchant.GetProperty("mm_cust_no").GetString();
            return GetPageData(userObj, merchantNo, pageItems);
        }

        public Hashtable GetPageData(JsonElement userObj, string merchantNo, Hashtable pageInfo)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter merchantAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant WHERE mm_cust_no = @CustNo ", conn);

            string search = "";
            Hashtable searchData = new Hashtable();
            if (pageInfo.ContainsKey("Search") && pageInfo["Search"] != null && pageInfo["Search"] is string)
            {
                search = (string)pageInfo["Search"];
                searchData = JsonSerializer.Deserialize<Hashtable>(search);
            }
            
          
            string tableName = (string) pageInfo["Name"];
            string sortField = (string) pageInfo["SortField"];
            string sortDirection = (string) pageInfo["SortDirection"];
            int page = Int32.Parse((string) pageInfo["Page"]) - 1;
             
            if(page < 0)
            {
                page = 0;
            }
            int pageSize = Int32.Parse((string) pageInfo["PageSize"]);
            CriteriaCollection criteriaCollection = new CriteriaCollection();

            string sql = "", countSql = "";
            if (tableName == "batch")
            {
                sql = "SELECT *  FROM ach_hist ";
                countSql = "SELECT  COUNT(*)  AS Count  FROM ach_hist  ";

                criteriaCollection.addCondition("ACCT_ACHACT", "Merchant_Number", "=", merchantNo);
                criteriaCollection.addCondition("DATE_ACHACT", "startDate", ">=", criteriaCollection.formatDate(searchData["batchStartDate"], "yyMMdd"));
                criteriaCollection.addCondition("DATE_ACHACT", "endDate", "<=", criteriaCollection.formatDate(searchData["batchEndDate"], "yyMMdd"));
            }
            else if (tableName == "transactions")
            {
                sql = "SELECT  *  FROM history   ";
                countSql = "SELECT  COUNT(*) AS Count FROM history  ";

                criteriaCollection.addCondition("merch_no_hf", "Merchant_Number", "=", merchantNo);
                criteriaCollection.addCondition("batch_date_hf", "startDate", ">=", criteriaCollection.formatDate(searchData["transactionStartDate"], "yyMMdd"));  
                criteriaCollection.addCondition("batch_date_hf", "endDate", "<=", criteriaCollection.formatDate(searchData["transactionEndDate"], "yyMMdd"));
                criteriaCollection.addCondition("card_no_hf", "card", " like ", "%" + searchData["transactionCard"] + "%");

            }
            else if (tableName == "chargebacks")
            {
                sql = "SELECT  *  FROM Daily_Disputes  ";
                countSql = "SELECT  COUNT(*) AS Count FROM Daily_Disputes   ";
                criteriaCollection.addCondition("Dispute_Type", "disputeType", " like ", "CB%");
                criteriaCollection.addCondition("Merchant_Number", "Merchant_Number", "=", merchantNo);
                criteriaCollection.addCondition("Dispute_Date", "startDate", ">=", criteriaCollection.formatDate(searchData["chargebacksStartDate"], "yyyyMMdd"));  
                criteriaCollection.addCondition("Dispute_Date", "endDate", "<=", criteriaCollection.formatDate(searchData["chargebacksEndDate"], "yyyyMMdd"));
                criteriaCollection.addCondition("Cardholder_Account_Number", "card", " like ", "%" + searchData["chargebacksCard"] + "%");
            }
            else if (tableName == "ach")
            {
                sql = "SELECT *  FROM ach_hist ";
                countSql = "SELECT  COUNT(*) AS Count FROM ach_hist   ";
                criteriaCollection.addCondition("ACCT_ACHACT", "Merchant_Number", "=", merchantNo);
                criteriaCollection.addCondition("DATE_ACHACT", "startDate", ">=", criteriaCollection.formatDate(searchData["achStartDate"], "yyMMdd"));  
                criteriaCollection.addCondition("DATE_ACHACT", "endDate", "<=", criteriaCollection.formatDate(searchData["achEndDate"], "yyMMdd"));   
            }
            sql += " WHERE " + criteriaCollection.getWhere();
            countSql += " WHERE " + criteriaCollection.getWhere();

            sql += " ORDER BY " + sortField + " " + sortDirection + " OFFSET " + (page * pageSize) + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";

            DataSet result = new DataSet();
            DataSet countResult = new DataSet();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn))
            {
                criteriaCollection.addParameterValues(dataAdapter.SelectCommand.Parameters);
                //dataAdapter.SelectCommand.Parameters.AddWithValue("@Merchant_Number", merchantNo);
                dataAdapter.SelectCommand.CommandTimeout = 0;
                dataAdapter.Fill(result);

            }

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(countSql, conn))
            {
                criteriaCollection.addParameterValues(dataAdapter.SelectCommand.Parameters);
                dataAdapter.SelectCommand.CommandTimeout = 0;
                dataAdapter.Fill(countResult);

            }

            toReturn.Add("SortDirection", sortDirection);
            toReturn.Add("Name", tableName);
            toReturn.Add("SortField", sortField);
            toReturn.Add("Page", page);
            toReturn.Add("PageSize", pageSize);
            toReturn.Add("Data", utilityManager.GetDataAsDynamic(result.Tables[0].Rows));
            toReturn.Add("Count", utilityManager.GetFirstValue(countResult.Tables[0].Rows, "Count"));

            return toReturn;
        }

        private Hashtable generateInitialPage(string tableName)
        {
            Hashtable toReturn = new Hashtable();
            toReturn.Add("Page", "1");
            toReturn.Add("PageSize", "10");
            toReturn.Add("Name", tableName);
            if (tableName == "batch")
            {
                toReturn.Add("SortField", "DATE_ACHACT");
                toReturn.Add("SortDirection", "DESC");
            }
            else if (tableName == "transactions")
            {
                toReturn.Add("SortField", "batch_date_hf");
                toReturn.Add("SortDirection", "DESC");

            }
            else if (tableName == "chargebacks")
            {
                toReturn.Add("SortField", "Dispute_Date");
                toReturn.Add("SortDirection", "DESC");

            }
            else if (tableName == "ach")
            {
                toReturn.Add("SortField", "DATE_ACHACT");
                toReturn.Add("SortDirection", "DESC");
            }

            toReturn.Add("Search", new Hashtable());
            return toReturn;
        }
        
        public Hashtable GetMerchantMaintainDetail(JsonElement userObj, JsonElement merchantContainer)
        {
            Hashtable toReturn = new Hashtable();
            string merchantNo = "";
            JsonElement permissions, permission;
            JsonElement merchant;
            JsonElement custNo;
            DataSet merchantResult = new DataSet(), isoResults = new DataSet();
            DataSet userResults = new DataSet();
            if (userObj.TryGetProperty("Permissions", out permissions))
            {
                if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "MERCHANT")
                {
                    merchantNo = userObj.GetProperty("UserDetail").GetProperty("mm_cust_no").GetString();
                }
                else if (merchantContainer.TryGetProperty("Merchant", out merchant) && merchant.TryGetProperty("mm_cust_no", out custNo))
                {
                    merchantNo = custNo.GetString();
                }

                if (merchantNo != null && merchantNo.Length > 0)
                {



                    using (SqlDataAdapter merchantAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant WHERE mm_cust_no = @CustNo ", conn))
                    {
                        merchantAdapter.SelectCommand.Parameters.AddWithValue("@CustNo", merchantNo);
                        merchantAdapter.SelectCommand.CommandTimeout = 0;
                        merchantAdapter.Fill(merchantResult);

                    }

                    using (SqlDataAdapter isoAdapter = new SqlDataAdapter("SELECT i.*  FROM ISO_MASTER i INNER JOIN ITS_Merchant m ON m.iso_code = i.ISO_CODE WHERE mm_cust_no = @CustNo ", conn))
                    {
                        isoAdapter.SelectCommand.Parameters.AddWithValue("@CustNo", merchantNo);
                        isoAdapter.SelectCommand.CommandTimeout = 0;
                        isoAdapter.Fill(isoResults);

                    }
                    using (SqlDataAdapter userAdapter = new SqlDataAdapter("SELECT u.*, p.sub_iso   FROM DAS_Users u INNER JOIN DAS_Users_By_Code_Permissions p ON p.Email_ID = u.Email_ID  WHERE mid = @Code  ", conn))
                    {
                        userAdapter.SelectCommand.Parameters.AddWithValue("@Code", merchantNo);
                        userAdapter.Fill(userResults);
                    }
                    List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
                    toReturn.Add("Users", utilityManager.GetDataAsDynamic(userResults.Tables[0].Rows));
                    toReturn.Add("Merchant", utilityManager.GetDataAsDynamic(merchantResult));
                    toReturn.Add("Iso", utilityManager.GetDataAsDynamic(isoResults));
                }
            }


            return toReturn;
        }


        public Hashtable GetMerchantDetail(JsonElement userObj, JsonElement merchantContainer)
        {
            Hashtable toReturn = new Hashtable();
            string merchantNo = "";
            JsonElement permissions, permission;
            JsonElement merchant = merchantContainer.GetProperty("Merchant");
            JsonElement custNo;
            DataSet merchantResult = new DataSet(), isoResults = new DataSet();

            if (userObj.TryGetProperty("Permissions", out permissions))
            {
                if (merchant.TryGetProperty("mm_cust_no", out custNo))
                {
                    merchantNo = merchant.GetProperty("mm_cust_no").GetString();
                }
                else if (permissions.TryGetProperty("User_Level_Code", out permission) && permission.GetString() == "MERCHANT")
                {
                    merchantNo = userObj.GetProperty("Permissions").GetProperty("mid").GetString();
                }
                
                if (merchantNo != null && merchantNo.Length > 0)
                {



                    using (SqlDataAdapter merchantAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant WHERE mm_cust_no = @CustNo ", conn))
                    {
                        merchantAdapter.SelectCommand.Parameters.AddWithValue("@CustNo", merchantNo);
                        merchantAdapter.SelectCommand.CommandTimeout = 0;
                        merchantAdapter.Fill(merchantResult);

                    }

                    using (SqlDataAdapter isoAdapter = new SqlDataAdapter("SELECT i.*  FROM ISO_MASTER i INNER JOIN ITS_Merchant m ON m.iso_code = i.ISO_CODE WHERE mm_cust_no = @CustNo ", conn))
                    {
                        isoAdapter.SelectCommand.Parameters.AddWithValue("@CustNo", merchantNo);
                        isoAdapter.SelectCommand.CommandTimeout = 0;
                        isoAdapter.Fill(isoResults);

                    }

                    Hashtable merchantDashboardData = this.GetMerchantDashboardData(merchantNo);
                    toReturn = utilityManager.addToHashtable(merchantDashboardData, toReturn);
                    toReturn.Add("Merchant", utilityManager.GetDataAsDynamic(merchantResult));
                    toReturn.Add("Iso", utilityManager.GetDataAsDynamic(isoResults));
                    toReturn.Add("transactions", GetPageData(userObj, merchantNo, generateInitialPage("transactions")));
                    toReturn.Add("chargebacks", GetPageData(userObj, merchantNo, generateInitialPage("chargebacks")));
                    toReturn.Add("ach", GetPageData(userObj, merchantNo, generateInitialPage("ach")));
                    toReturn.Add("batch", GetPageData(userObj, merchantNo, generateInitialPage("batch")));
                }
            }
 

            return toReturn;
        }

         
    }
}
