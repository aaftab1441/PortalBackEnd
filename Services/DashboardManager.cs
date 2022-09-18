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
    public class DashboardManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public DashboardManager(string connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }

        ~DashboardManager()
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
        public Hashtable GetMerchantDashboardData(JsonElement userObj, string merchantNumber)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter dashboardAdapter, firstAccountAdapter;
            JsonElement userDetails, email = new JsonElement();
            string emailId = "";
            DataSet dashboardData = new DataSet(), firstAccountData = new DataSet();
            if (userObj.TryGetProperty("UserDetail", out userDetails) && userDetails.TryGetProperty("Email_ID", out email))
            {
                emailId = email.GetString();
            }

            string dashboardQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode ORDER BY category, variable_name, period ";
            if (merchantNumber != null && merchantNumber.Length > 0)
            {
                dashboardAdapter = new SqlDataAdapter(dashboardQuery, conn);
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "MCT");
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", merchantNumber);
                dashboardAdapter.Fill(dashboardData);
                dashboardAdapter.Dispose();

            } else if (emailId != null && emailId.Length > 0)
            {


                firstAccountAdapter = new SqlDataAdapter("SELECT TOP 1 mid FROM DAS_Users_By_Code_Permissions WHERE Email_ID = @Email_ID", conn);
                firstAccountAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailId);
                firstAccountAdapter.Fill(firstAccountData);
                firstAccountAdapter.Dispose();
                merchantNumber = (string)utilityManager.GetFirstValue(firstAccountData.Tables[0].Rows, "mid");

                dashboardAdapter = new SqlDataAdapter(dashboardQuery, conn);
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "MCT");
                dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", merchantNumber);
                dashboardAdapter.Fill(dashboardData);
                dashboardAdapter.Dispose();

            }

            SqlDataAdapter merchantListAdapter = new SqlDataAdapter("SELECT *  FROM ITS_Merchant i INNER JOIN DAS_Users_By_Code_Permissions p ON p.mid = i.mm_cust_no WHERE Email_ID = @Email_ID ", conn);
            if (emailId != null && emailId.Length > 0)
            {
                merchantListAdapter.SelectCommand.Parameters.AddWithValue("@Email_ID", emailId);
                DataSet merchantList = new DataSet();
                merchantListAdapter.Fill(merchantList);

                toReturn.Add("MerchantList", utilityManager.GetDataAsDynamic(merchantList.Tables[0].Rows));
            }

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData = utilityManager.CategorizeData(allData);
            //GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost");

            toReturn.Add("CurrentMid", merchantNumber);

            toReturn.Add("CurrentYTDSales", utilityManager.GetValue(categorizedData, "Sales", "Current_YTD"));
            toReturn.Add("PreviousYTDSales", utilityManager.GetValue(categorizedData, "Sales", "Previous_YTD"));

            toReturn.Add("CurrentYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Last_Year_Past_Six_Months"), true));

            toReturn.Add("TransactionCount", utilityManager.GetValue(categorizedData, "Transaction_Counts", "Current_Month"));
            toReturn.Add("ChargeBackCount", utilityManager.GetValue(categorizedData, "ChargeBacks", "Current_ChargeBacks"));

            toReturn.Add("PreviousYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Previous_ChargeBacks_Chart"), true));
            toReturn.Add("CurrentYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Current_ChargeBacks_Chart"), false));

            toReturn.Add("CurrentYTDTransactions", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Transaction_Counts", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTDTransactions", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Transaction_Counts", "Last_Year_Past_Six_Months"), true));

            return toReturn;
        }
        public Hashtable GetMainIsoDashboardData(string isoCode)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter dashboardAdapter;

            DataSet dashboardData = new DataSet(), isoData = new DataSet();

            string dashboardQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode ORDER BY category, variable_name, period ";
            dashboardAdapter = new SqlDataAdapter(dashboardQuery, conn);
            dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "ISO");
            dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", isoCode);
            dashboardAdapter.Fill(dashboardData);
            dashboardAdapter.Dispose();

            string isoDetailQuery = "SELECT * FROM ISO_MASTER WHERE ISO_CODE = @isoCode";
            SqlDataAdapter isoAdapter = new SqlDataAdapter(isoDetailQuery, conn);
            isoAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", isoCode);
            isoAdapter.Fill(isoData);
            isoAdapter.Dispose();
            toReturn.Add("Detail", utilityManager.GetDataAsDynamic(isoData));

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData = utilityManager.CategorizeData(allData);
            //GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost");
            toReturn.Add("TotalAssoc", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost"));
            toReturn.Add("TotalSchedAFees", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Total_Sched_A_Fees"));
            toReturn.Add("RevenueShare", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Revenue_Share"));
            toReturn.Add("NetIncome", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Net_Income"));
            toReturn.Add("DasRevenueShare", utilityManager.GetChartData(categorizedData, "Revenue_Share", "DAS_Revenue_Share"));

            toReturn.Add("CurrentYTDSales", utilityManager.GetValue(categorizedData, "Sales", "Current_YTD"));
            toReturn.Add("PreviousYTDSales", utilityManager.GetValue(categorizedData, "Sales", "Previous_YTD"));

            toReturn.Add("CurrentYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Last_Year_Past_Six_Months"), true));

            toReturn.Add("TotalMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Total"));
            toReturn.Add("ActiveMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Active"));
            toReturn.Add("ClosedMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Closed"));
            toReturn.Add("InActiveMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Inactive"));

            toReturn.Add("PreviousTotalMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Total"));
            toReturn.Add("PreviousActiveMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Active"));
            toReturn.Add("PreviousClosedMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Closed"));
            toReturn.Add("PreviousInActiveMerchants", utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Inactive"));


            toReturn.Add("TransactionCount", utilityManager.GetValue(categorizedData, "Transaction_Counts", "Current_Month"));
            toReturn.Add("ChargeBackCount", utilityManager.GetValue(categorizedData, "ChargeBacks", "Current_ChargeBacks"));

            toReturn.Add("PreviousYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Previous_ChargeBacks_Chart"), true));
            toReturn.Add("CurrentYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Current_ChargeBacks_Chart"), false));

            return toReturn;
        }
        public Hashtable GetDashboardData()
        { 
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter dashboardAdapter;

            DataSet dashboardData = new DataSet();
            
            string dashboardQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode ORDER BY category, variable_name, period ";
            dashboardAdapter = new SqlDataAdapter(dashboardQuery, conn);
            dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "DAS");
            dashboardAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", "001");
            dashboardAdapter.Fill(dashboardData);
            dashboardAdapter.Dispose();

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData =  utilityManager.CategorizeData(allData);
            //GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost");
             toReturn.Add("TotalAssoc", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost"));
             toReturn.Add("TotalSchedAFees", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Total_Sched_A_Fees"));
             toReturn.Add("RevenueShare", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Revenue_Share"));
             toReturn.Add("NetIncome", utilityManager.GetChartData(categorizedData, "Revenue_Share", "Net_Income"));
             toReturn.Add("DasRevenueShare", utilityManager.GetChartData(categorizedData, "Revenue_Share", "DAS_Revenue_Share"));

             toReturn.Add("CurrentYTDSales",  utilityManager.GetValue(categorizedData, "Sales", "Current_YTD"));
             toReturn.Add("PreviousYTDSales",  utilityManager.GetValue(categorizedData, "Sales", "Previous_YTD"));

            toReturn.Add("CurrentYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Past_Six_Months"), false));
            toReturn.Add("PreviousYTD", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "Sales", "Last_Year_Past_Six_Months"), true));

            toReturn.Add("TotalMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Total"));
            toReturn.Add("ActiveMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Active"));
            toReturn.Add("ClosedMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Closed"));
            toReturn.Add("InActiveMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Inactive"));

            toReturn.Add("PreviousTotalMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Total"));
            toReturn.Add("PreviousActiveMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Active"));
            toReturn.Add("PreviousClosedMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Closed"));
            toReturn.Add("PreviousInActiveMerchants",  utilityManager.GetValue(categorizedData, "Merchant_Counts", "Previous_Inactive"));

            toReturn.Add("TransactionCount",  utilityManager.GetValue(categorizedData, "Transaction_Counts", "Current_Month"));
            toReturn.Add("ChargeBackCount",  utilityManager.GetValue(categorizedData, "ChargeBacks", "Current_ChargeBacks"));
            
            toReturn.Add("PreviousYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Previous_ChargeBacks_Chart"), true));
            toReturn.Add("CurrentYTDChargeBacks", utilityManager.processDateConversion(utilityManager.GetChartData(categorizedData, "ChargeBacks", "Current_ChargeBacks_Chart"), false));

 
            return toReturn;
        }

        

      
     

        public Hashtable GetIsoListDashboardData()
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter previousYTDAdapter, currentYTDAdapter;

            DataSet currentYTDDataSet = new DataSet();

            //'current YTD
            currentYTDAdapter = new SqlDataAdapter("SELECT value Transaction_Amount, org_code ISO_CODE, variable_name ISO_NAME FROM Dashboard_Data WHERE org_type = 'DAS' AND  category = 'ISO_List_Current_YTD_Sales' ORDER BY  variable_name  ", conn);
            currentYTDAdapter.SelectCommand.CommandTimeout = 0;
            currentYTDAdapter.Fill(currentYTDDataSet);
            currentYTDAdapter.Dispose();

            List<Dictionary<string, object>> salesData = utilityManager.GetDataAsDynamic(currentYTDDataSet.Tables[0].Rows);
            List<dynamic> finalSalesData = new List<dynamic>();
            Dictionary<string, Object> aRow;

            double salesAmount;
            Object salesAmountObj;
            DataSet previousYTDDataSet;
            for (int i = 0; i < salesData.Count - 1; i++)
            {
                aRow = salesData[i];
                previousYTDDataSet = new DataSet();
                previousYTDAdapter = new SqlDataAdapter("SELECT value Transaction_Amount FROM Dashboard_Data WHERE org_type = 'DAS' AND  category = 'ISO_List_Previous_YTD_Sales' AND org_code = @isoCode ", conn);
                previousYTDAdapter.SelectCommand.CommandTimeout = 0;
                previousYTDAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", aRow["ISO_CODE"]);
                previousYTDAdapter.Fill(previousYTDDataSet);
                previousYTDAdapter.Dispose();
                salesAmount = 0;
                salesAmountObj = utilityManager.GetFirstValue(previousYTDDataSet.Tables[0].Rows, "Transaction_Amount");
                if (!Convert.IsDBNull(salesAmountObj) && salesAmountObj != null)
                {
                    salesAmount = Decimal.ToDouble(Convert.ToDecimal(salesAmountObj));
                }
                aRow.Add("Previous_Transaction_Amount", salesAmount);
                finalSalesData.Add(aRow);
            }
            toReturn.Add("SalesData", finalSalesData);

            return toReturn;
        }

        public Hashtable GetIsoMerchantCountsByStatus(string[] parts)
        {
            Hashtable toReturn = new Hashtable();
            string merchantStatus = "all";
            if (parts.Length > 1)
            {
                merchantStatus = parts[1];
            }
            SqlDataAdapter merchantCountAdapter = new SqlDataAdapter();
            DataSet merchantListDataSet = new DataSet();
            string isoCode = "";
            merchantCountAdapter = new SqlDataAdapter("select  Count(*) Merchant_Count, s.ISO_CODE, s.ISO_NAME  FROM [ITS_merchant]  i " +
                " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
                " WHERE i.mm_status_code LIKE @st GROUP BY s.ISO_CODE, s.ISO_NAME  ", conn);

            if (merchantStatus == "all")
            {
                merchantStatus = "%";
            }
            else if (merchantStatus == "active")
            {
                merchantStatus = "A";
            }
            else if (merchantStatus == "closed")
            {
                merchantStatus = "C";
            }
            else if (merchantStatus == "inactive")
            {
                merchantStatus = "S";
            }

            merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@st", merchantStatus);
            merchantCountAdapter.Fill(merchantListDataSet);
            merchantCountAdapter.Dispose();
            toReturn.Add("Data", utilityManager.GetDataAsDynamic(merchantListDataSet.Tables[0].Rows));


            toReturn.Add("TotalCount", GetMerchantCounts(isoCode, "%"));
            toReturn.Add("ActiveCount", GetMerchantCounts(isoCode, "A"));
            toReturn.Add("ClosedCount", GetMerchantCounts(isoCode, "C"));
            toReturn.Add("InactiveCount", GetMerchantCounts(isoCode, "S"));


            return toReturn;
        }

        public int GetMerchantCounts(string isoCode, string status)
        {
            SqlDataAdapter merchantCountAdapter = new SqlDataAdapter();
            DataSet merchantCountDataSet = new DataSet();
            int count = 0;
            if (isoCode.Length > 0)
            {
                merchantCountAdapter = new SqlDataAdapter("select Count(*) Merchant_Count FROM [ITS_merchant] WHERE  mm_status_code LIKE @st AND iso _code = @isoCode ", conn);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@st", status);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", isoCode);
            }
            else
            {
                merchantCountAdapter = new SqlDataAdapter("select Count(*) Merchant_Count FROM [ITS_merchant] WHERE  mm_status_code like @st ", conn);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@st", status);

            }
            merchantCountAdapter.Fill(merchantCountDataSet);
            merchantCountAdapter.Dispose();
            count = (int)utilityManager.GetFirstValue(merchantCountDataSet.Tables[0].Rows, "Merchant_Count");
            return count;
        }

        public Hashtable GetIsoDetailDashboardData(string isoCode)
        {
            Hashtable toReturn = new Hashtable();
            string dateStr;

            SqlDataAdapter totalMerchantsAdapter, activeMerchantsAdapter, closedMerchantsAdapter, previousClosedMerchantsAdapter, inActiveMerchantsAdapter,
                previousActiveMerchantsAdapter, previousInActiveMerchantsAdapter, previousTotalMerchantsAdapter, previousYTDAdapter,
            currentYTDAdapter, transactionCountAdapter, previousMonthChargeBackAdapter, previousYTDChargeBackAdapter, currentMonthChargeBackAdapter, currentYTDChargeBackAdapter;

            DataSet previousMonthChargeBackDataSet = new DataSet();
            DataSet previousYTDChargeBackDataSet = new DataSet();
            DataSet currentMonthChargeBackDataSet = new DataSet();
            DataSet currentYTDChargeBackDataSet = new DataSet();

            DataSet currentYTDDataSet = new DataSet();

            DataSet previousYTDDataSet = new DataSet();
            DataSet transactionCountDataSet = new DataSet();

            DataSet totalMerchantsCounts = new DataSet();
            DataSet activeMerchantsCounts = new DataSet();
            DataSet closedMerchantsCounts = new DataSet();
            DataSet inactiveMerchantsCounts = new DataSet();
            DataSet chargeBackCount = new DataSet();

            DataSet previousMerchantsCounts = new DataSet();
            DataSet previousActiveMerchantsCounts = new DataSet();
            DataSet previousClosedMerchantsCounts = new DataSet();
            DataSet previousInactiveMerchantsCounts = new DataSet();

            dateStr = DateTime.Now.AddDays(-90).ToString("yyyyMMdd");
            DateTime currentStartMonth = System.DateTime.Now.AddMonths(-6);// '-Convert.ToDouble(System.DateTime.Now.Day - 1));


            string currentStartMonthStr = currentStartMonth.ToString("yyyyMMdd");
            string currentStartMonthMMDDYYStr = currentStartMonth.ToString("MMddyy");
            string currentStartMonthYYMMStr = currentStartMonth.ToString("yyMMdd");
            DateTime previous6StartMonth = currentStartMonth.AddYears(-1);
            string previous6StartMonthStr = previous6StartMonth.ToString("yyMMdd");

            DateTime currentDate = DateTime.Now;
            string currentDateStr = currentDate.ToString("yyyyMMdd");
            string currentYear = currentStartMonth.ToString("yyyy");
            DateTime previous6MonthCurrentDate = currentDate.AddYears(-1);

            DateTime sixMonthsAgoDate = DateTime.Now.AddMonths(-6);
            DateTime previousStartOfYearDate = sixMonthsAgoDate.AddYears(-1);
            string priorYearStartOf6Months = previousStartOfYearDate.ToString("yyMMdd");
            string sixMonthsAgoDateStr = sixMonthsAgoDate.ToString("yyMMdd");
            string previousYearCurrentDayStr = currentDate.AddYears(-1).ToString("yyMMdd");
            string sixMonthsMMDDYYStr = currentStartMonth.ToString("MMddyy");

            DataSet totalAssocDataSet = new DataSet();
            DataSet totalSchedAFeesDataSet = new DataSet();
            DataSet totalIncomeDataSet = new DataSet();
            DataSet netIncomeDataSet = new DataSet();
            DataSet dashRevenueShareDataSet = new DataSet();
            DataSet revenueShareDataSet = new DataSet();

            SqlDataAdapter revenueShare = new SqlDataAdapter("select mm_cust_no, mm_dba_name, sum(Total_Expense) Total_Expense, sum(Total_Sales_Volume) Total_Sales_Volume, sum(Total_Return_Amt) Total_Return_Amt, " +
                " sum([Total_Assoc_Cost]) Total_Assoc_Cost, sum([Total_Sched_A_Fees]) Total_Sched_A_Fees, " +
                " sum([Total_Income]) Total_Income, sum([Net_Income]) Net_Income, sum([Revenue_Share]) Revenue_Share, sum([DAS_Revenue_Share]) DAS_Revenue_Share FROM Dashboard_Data_Revenue_Share r" +
                " WHERE ISO_Code = @iso  GROUP BY mm_cust_no, mm_dba_name ", conn);
            revenueShare.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            revenueShare.Fill(revenueShareDataSet);
            revenueShare.Dispose();

            string totalAssocCostQuery = "select sum([Total_Assoc_Cost]) y, Month_end_date x FROM Dashboard_Data_Revenue_Share WHERE Month_end_date >= @st AND ISO_Code = @iso GROUP BY Month_end_date order by x";
            SqlDataAdapter totalAssocSqlAdapter = new SqlDataAdapter(totalAssocCostQuery, conn);
            totalAssocSqlAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            totalAssocSqlAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            totalAssocSqlAdapter.Fill(totalAssocDataSet);
            totalAssocSqlAdapter.Dispose();

            string totalSchedAFeesQuery = "select sum([Total_Sched_A_Fees]) y, Month_end_date x  FROM Dashboard_Data_Revenue_Share  WHERE Month_end_date >= @st  AND ISO_Code = @iso  GROUP BY  Month_end_date order by x";
            SqlDataAdapter totalSchedAFeesSqlAdapter = new SqlDataAdapter(totalSchedAFeesQuery, conn);
            totalSchedAFeesSqlAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            totalSchedAFeesSqlAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            totalSchedAFeesSqlAdapter.Fill(totalSchedAFeesDataSet);
            totalSchedAFeesSqlAdapter.Dispose();

            string totalIncomeQuery = "select sum([Total_Income]) y, Month_end_date x  FROM Dashboard_Data_Revenue_Share  WHERE Month_end_date >= @st  AND ISO_Code = @iso group by Month_end_date order by x";
            SqlDataAdapter totalIncomeAdapter = new SqlDataAdapter(totalIncomeQuery, conn);
            totalIncomeAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            totalIncomeAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            totalIncomeAdapter.Fill(totalIncomeDataSet);
            totalIncomeAdapter.Dispose();

            string netIncomeQuery = "select sum([Net_Income]) y, Month_end_date x FROM Dashboard_Data_Revenue_Share   WHERE Month_end_date >= @st   AND ISO_Code = @iso  group by Month_end_date order by x";
            SqlDataAdapter netIncomeSqlAdapter = new SqlDataAdapter(netIncomeQuery, conn);
            netIncomeSqlAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            netIncomeSqlAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            netIncomeSqlAdapter.Fill(netIncomeDataSet);
            netIncomeSqlAdapter.Dispose();

            string revenueShareQuery = "select sum([Revenue_Share]) y, Month_end_date x FROM Dashboard_Data_Revenue_Share   WHERE Month_end_date >= @st  AND ISO_Code = @iso  group by Month_end_date order by x";
            SqlDataAdapter revenueShareSqlAdapter = new SqlDataAdapter(revenueShareQuery, conn);
            revenueShareSqlAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            revenueShareSqlAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            revenueShareSqlAdapter.Fill(revenueShareDataSet);
            revenueShareSqlAdapter.Dispose();

            string dasRevenueSharetQuery = "select sum([DAS_Revenue_Share]) y, Month_end_date x  FROM Dashboard_Data_Revenue_Share  WHERE Month_end_date >= @st  AND ISO_Code = @iso  group by Month_end_date order by x";
            SqlDataAdapter dasRevenueShareAdapter = new SqlDataAdapter(dasRevenueSharetQuery, conn);
            dasRevenueShareAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthMMDDYYStr);
            dasRevenueShareAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            dasRevenueShareAdapter.Fill(dashRevenueShareDataSet);
            dasRevenueShareAdapter.Dispose();

            SqlDataAdapter chargeBacksAdapter = new SqlDataAdapter("SELECT SUM(value) Charge_Back_Count, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer   " +
                " WHERE variable = @variable AND month_end >= @st AND iso_code = @iso " +
                " GROUP BY mm_cust_no, mm_legal_name, mm_dba_name ORDER BY Charge_Back_Count desc ", conn);
            chargeBacksAdapter.SelectCommand.Parameters.AddWithValue("@variable", "ChargeBacks");
            chargeBacksAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonthYYMMStr);
            chargeBacksAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            chargeBacksAdapter.Fill(chargeBackCount);
            chargeBacksAdapter.Dispose();

            previousMonthChargeBackAdapter = new SqlDataAdapter("SELECT SUM(value)  Charge_Back_Count, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer   " +
                " WHERE variable = @variable AND month_end >= @st AND month_end <= @et AND iso_code = @iso " +
                " GROUP BY mm_cust_no, mm_legal_name, mm_dba_name ORDER BY Charge_Back_Count desc ", conn);
            previousMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@variable", "ChargeBacks");
            previousMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@st", previous6StartMonth.ToString("yyMMdd"));
            previousMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@et", previous6MonthCurrentDate.ToString("yyMMdd"));
            previousMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousMonthChargeBackAdapter.Fill(previousMonthChargeBackDataSet);
            previousMonthChargeBackAdapter.Dispose();

            previousYTDChargeBackAdapter = new SqlDataAdapter("SELECT SUM(value)  Charge_Back_Count, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer   " +
                " WHERE variable = @variable AND month_end >= @st AND month_end <= @et  AND iso_code = @iso " +
                " GROUP BY mm_cust_no, mm_legal_name, mm_dba_name ORDER BY Charge_Back_Count desc ", conn);
            previousYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@variable", "ChargeBacks");
            previousYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@st", previousStartOfYearDate.ToString("yyMMdd"));
            previousYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@et", currentDate.AddYears(-1).ToString("yyMMdd"));
            previousYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousYTDChargeBackAdapter.Fill(previousYTDChargeBackDataSet);
            previousYTDChargeBackAdapter.Dispose();


            currentMonthChargeBackAdapter = new SqlDataAdapter("SELECT SUM(value)  Charge_Back_Count, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer   " +
                " WHERE variable = @variable AND month_end >= @st AND month_end <= @et  AND iso_code = @iso " +
                " GROUP BY mm_cust_no, mm_legal_name, mm_dba_name ORDER BY Charge_Back_Count desc ", conn);
            currentMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@variable", "ChargeBacks");
            currentMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@st", currentStartMonth.ToString("yyMMdd"));
            currentMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@et", currentDate.ToString("yyMMdd"));
            currentMonthChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            currentMonthChargeBackAdapter.Fill(currentMonthChargeBackDataSet);
            currentMonthChargeBackAdapter.Dispose();

            currentYTDChargeBackAdapter = new SqlDataAdapter("SELECT SUM(value) Charge_Back_Count, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer   " +
                " WHERE variable = @variable AND month_end >= @st  AND month_end <= @et AND iso_code = @iso " +
                " GROUP BY mm_cust_no, mm_legal_name, mm_dba_name ORDER BY Charge_Back_Count desc ", conn);
            currentYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@variable", "ChargeBacks");
            currentYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@st", sixMonthsAgoDate.ToString("yyMMdd"));
            currentYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@et", currentDate.ToString("yyMMdd"));
            currentYTDChargeBackAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            currentYTDChargeBackAdapter.Fill(currentYTDChargeBackDataSet);
            currentYTDChargeBackAdapter.Dispose();

            totalMerchantsAdapter = new SqlDataAdapter("select i.mm_cust_no, i.mm_legal_name, i.mm_dba_name  FROM [ITS_merchant] i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
            " WHERE s.ISO_CODE = @iso " +
            " GROUP BY i.mm_cust_no, i.mm_legal_name, i.mm_dba_name ", conn);
            totalMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            totalMerchantsAdapter.Fill(totalMerchantsCounts);
            totalMerchantsAdapter.Dispose();

            activeMerchantsAdapter = new SqlDataAdapter("select Count(*) Merchant_Count, i.mm_cust_no, i.mm_legal_name, i.mm_dba_name FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
            " WHERE mm_status_code = @st  AND s.ISO_CODE = @iso  GROUP BY i.mm_cust_no, i.mm_legal_name, i.mm_dba_name ORDER BY Merchant_Count desc ", conn);
            activeMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "A");
            activeMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            activeMerchantsAdapter.Fill(activeMerchantsCounts);
            activeMerchantsAdapter.Dispose();

            closedMerchantsAdapter = new SqlDataAdapter("select Count(*) Merchant_Count, i.mm_cust_no, i.mm_legal_name, i.mm_dba_name   FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code  " +
            " WHERE mm_status_code = @st  AND s.ISO_CODE = @iso  GROUP BY i.mm_cust_no, i.mm_legal_name, i.mm_dba_name ORDER BY Merchant_Count desc  ", conn);
            closedMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "C");
            closedMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            closedMerchantsAdapter.Fill(closedMerchantsCounts);
            closedMerchantsAdapter.Dispose();

            inActiveMerchantsAdapter = new SqlDataAdapter("select Count(*) Merchant_Count , i.mm_cust_no, i.mm_legal_name, i.mm_dba_name  FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
            " WHERE mm_status_code = @st AND s.ISO_CODE = @iso GROUP BY i.mm_cust_no, i.mm_legal_name, i.mm_dba_name ORDER BY Merchant_Count desc  ", conn);
            inActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "S");
            inActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            inActiveMerchantsAdapter.Fill(inactiveMerchantsCounts);
            inActiveMerchantsAdapter.Dispose();

            previousTotalMerchantsAdapter = new SqlDataAdapter("select i.mm_cust_no, i.mm_legal_name, i.mm_dba_name, i.mm_install_date FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
            " WHERE mm_status_date < @et AND s.ISO_CODE = @iso ORDER BY i.mm_install_date desc", conn);
            previousTotalMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@et", currentStartMonthYYMMStr);
            previousTotalMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousTotalMerchantsAdapter.Fill(previousMerchantsCounts);
            previousTotalMerchantsAdapter.Dispose();

            previousActiveMerchantsAdapter = new SqlDataAdapter("select i.mm_cust_no, i.mm_legal_name, i.mm_dba_name, i.mm_install_date  FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code  " +
            " WHERE mm_status_code = @st AND mm_status_date < @et AND s.ISO_CODE = @iso   ORDER BY i.mm_install_date desc", conn);
            previousActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "A");
            previousActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@et", currentStartMonthYYMMStr);
            previousActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousActiveMerchantsAdapter.Fill(previousActiveMerchantsCounts);
            previousActiveMerchantsAdapter.Dispose();

            previousClosedMerchantsAdapter = new SqlDataAdapter("select i.mm_cust_no, i.mm_legal_name, i.mm_dba_name, i.mm_install_date   FROM [ITS_merchant]  i " +
                "INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code " +
                "WHERE mm_status_code = @st AND mm_status_date < @et AND s.ISO_CODE = @iso  ORDER BY i.mm_install_date desc ", conn);
            previousClosedMerchantsAdapter.SelectCommand.CommandTimeout = 0;
            previousClosedMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "C");
            previousClosedMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@et", currentStartMonthYYMMStr);
            previousClosedMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousClosedMerchantsAdapter.Fill(previousClosedMerchantsCounts);
            previousClosedMerchantsAdapter.Dispose();

            previousInActiveMerchantsAdapter = new SqlDataAdapter("select i.mm_cust_no, i.mm_legal_name, i.mm_dba_name, i.mm_install_date   FROM [ITS_merchant]  i " +
            " INNER JOIN ISO_MASTER s ON s.ISO_CODE = i.iso_code  AND s.ISO_CODE = @iso  WHERE i.mm_status_code = @st AND i.mm_status_date< @et " +
            " AND  s.ISO_CODE = @iso " +
            " ORDER BY i.mm_install_date desc  ", conn);
            previousInActiveMerchantsAdapter.SelectCommand.CommandTimeout = 0;
            previousInActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@st", "S");
            previousInActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@et", currentStartMonthYYMMStr);
            previousInActiveMerchantsAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            previousInActiveMerchantsAdapter.Fill(previousInactiveMerchantsCounts);
            previousInActiveMerchantsAdapter.Dispose();

            conn.CreateCommand().CommandTimeout = 500;

            //'current YTD
            currentYTDAdapter = new SqlDataAdapter("SELECT TOP 30 Sum(value) Transaction_Amount, mm_cust_no, mm_legal_name, mm_dba_name FROM Dashboad_Data_ISO_Customer    " +
                " WHERE month_end >= @st AND iso_code = @iso AND variable = @variable" +
                " GROUP BY  mm_cust_no, mm_legal_name, mm_dba_name ", conn);
            currentYTDAdapter.SelectCommand.CommandTimeout = 0;
            currentYTDAdapter.SelectCommand.Parameters.AddWithValue("@st", sixMonthsAgoDateStr);
            currentYTDAdapter.SelectCommand.Parameters.AddWithValue("@iso", isoCode);
            currentYTDAdapter.SelectCommand.Parameters.AddWithValue("@variable", "Transaction_Amount");            
            currentYTDAdapter.Fill(currentYTDDataSet);
            currentYTDAdapter.Dispose();

            List<Dictionary<string, object>> salesData = utilityManager.GetDataAsDynamic(currentYTDDataSet.Tables[0].Rows);
            List<dynamic> finalSalesData = new List<dynamic>();
            Dictionary<string, Object> aRow;


            double salesAmount;
            Object salesValue;

            for (int i = 0; i < salesData.Count - 1; i++)
            {
                aRow = salesData[i];
                previousYTDDataSet = new DataSet();
                previousYTDAdapter = new SqlDataAdapter("select Sum(value) Transaction_Amount   FROM Dashboad_Data_ISO_Customer " +
                    "WHERE mm_cust_no = @merch_no_hf AND month_end >= @st AND  month_end<@et AND variable = @variable ", conn);
                previousYTDAdapter.SelectCommand.CommandTimeout = 0;
                previousYTDAdapter.SelectCommand.Parameters.AddWithValue("@st", priorYearStartOf6Months);
                previousYTDAdapter.SelectCommand.Parameters.AddWithValue("@et", previousYearCurrentDayStr);
                previousYTDAdapter.SelectCommand.Parameters.AddWithValue("@merch_no_hf", aRow["mm_cust_no"]);
                previousYTDAdapter.SelectCommand.Parameters.AddWithValue("@variable", "Transaction_Amount");

                previousYTDAdapter.Fill(previousYTDDataSet);
                previousYTDAdapter.Dispose();
                salesAmount = 0;
                salesValue = utilityManager.GetFirstValue(previousYTDDataSet.Tables[0].Rows, "Transaction_Amount");
                if (!Convert.IsDBNull(salesValue))
                {
                    salesAmount = (double)salesValue;
                }
                aRow.Add("Previous_Transaction_Amount", salesAmount);
                finalSalesData.Add(aRow);
            }


            toReturn.Add("SalesData", finalSalesData);
            //'toReturn.Add("PreviousYTD", utilityManager.GetDataAsDynamic(previousYTDDataSet.Tables[0].Rows));
            toReturn.Add("RevenueShare", utilityManager.GetDataAsDynamic(revenueShareDataSet.Tables[0].Rows));


            toReturn.Add("TotalMerchants", utilityManager.GetDataAsDynamic(totalMerchantsCounts.Tables[0].Rows));
            toReturn.Add("ActiveMerchants", utilityManager.GetDataAsDynamic(activeMerchantsCounts.Tables[0].Rows));
            toReturn.Add("ClosedMerchants", utilityManager.GetDataAsDynamic(closedMerchantsCounts.Tables[0].Rows));
            toReturn.Add("InActiveMerchants", utilityManager.GetDataAsDynamic(inactiveMerchantsCounts.Tables[0].Rows));

            toReturn.Add("PreviousTotalMerchants", utilityManager.GetDataAsDynamic(previousMerchantsCounts.Tables[0].Rows));
            toReturn.Add("PreviousActiveMerchants", utilityManager.GetDataAsDynamic(previousActiveMerchantsCounts.Tables[0].Rows));
            toReturn.Add("PreviousClosedMerchants", utilityManager.GetDataAsDynamic(previousClosedMerchantsCounts.Tables[0].Rows));
            toReturn.Add("PreviousInActiveMerchants", utilityManager.GetDataAsDynamic(previousInactiveMerchantsCounts.Tables[0].Rows));

            //'toReturn.Add("TransactionCount", utilityManager.GetDataAsDynamic(transactionCountDataSet.Tables[0].Rows));
            toReturn.Add("ChargeBackCount", utilityManager.GetDataAsDynamic(chargeBackCount.Tables[0].Rows));
            toReturn.Add("PreviousMonthChargeBacks", utilityManager.GetDataAsDynamic(previousMonthChargeBackDataSet.Tables[0].Rows));
            toReturn.Add("PreviousYTDChargeBacks", utilityManager.GetDataAsDynamic(previousYTDChargeBackDataSet.Tables[0].Rows));
            toReturn.Add("CurrentMonthChargeBacks", utilityManager.GetDataAsDynamic(currentMonthChargeBackDataSet.Tables[0].Rows));
            toReturn.Add("CurrentYTDChargeBacks", utilityManager.GetDataAsDynamic(currentYTDChargeBackDataSet.Tables[0].Rows));



            return toReturn;
        }
    }
}
