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
    public class RiskManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public RiskManager(string connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }

        ~RiskManager()
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
        public List<Dictionary<string, object>> GetMerchantRisk(string merchantNumber)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter riskAdapter;
            
            DataSet dashboardData = new DataSet(), firstAccountData = new DataSet();
            

            string riskQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode AND category = @category  ORDER BY category, variable_name, period ";
            if(merchantNumber != null && merchantNumber.Length > 0)
            {
                riskAdapter = new SqlDataAdapter(riskQuery, conn);
                riskAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "MCT");
                riskAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", merchantNumber);
                riskAdapter.SelectCommand.Parameters.AddWithValue("@category", "Risk");
                riskAdapter.Fill(dashboardData);
                riskAdapter.Dispose();

            }

            List <Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData =  utilityManager.CategorizeData(allData);
            //GetChartData(categorizedData, "Revenue_Share", "Total_Assoc_Cost");

            return utilityManager.GetRiskData(categorizedData);           
        }
        public List<Dictionary<string, object>> GetMainIsoDashboardData(string isoCode)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter riskAdapter;

            DataSet dashboardData = new DataSet();

            string riskQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode AND category = @category  ORDER BY category, variable_name, period ";
            riskAdapter = new SqlDataAdapter(riskQuery, conn);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "ISO");
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", isoCode);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@category", "Risk");
            riskAdapter.Fill(dashboardData);
            riskAdapter.Dispose();

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData =  utilityManager.CategorizeData(allData);
            
           return  utilityManager.GetRiskData(categorizedData);
        }
        public List<Dictionary<string, object>> Get13MonthHistory()
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter riskAdapter;

            DataSet dashboardData = new DataSet();
            
            string riskQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode AND category = @category ORDER BY category, variable_name, period ";
            riskAdapter = new SqlDataAdapter(riskQuery, conn);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgType", "DAS");
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", "001");
            riskAdapter.SelectCommand.Parameters.AddWithValue("@category", "Risk");
            riskAdapter.Fill(dashboardData);
            riskAdapter.Dispose();

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData =  utilityManager.CategorizeData(allData);

            return utilityManager.GetRiskData(categorizedData);
        }

        public List<Dictionary<string, object>> Get13MonthHistory(string orgType, string orgCode)
        {
            Hashtable toReturn = new Hashtable();
            SqlDataAdapter riskAdapter;

            DataSet dashboardData = new DataSet();

            string riskQuery = "SELECT * FROM Dashboard_Data WHERE org_type = @orgType AND org_code = @orgCode AND category = @category ORDER BY category, variable_name, period ";
            riskAdapter = new SqlDataAdapter(riskQuery, conn);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgType", orgType);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@orgCode", orgCode);
            riskAdapter.SelectCommand.Parameters.AddWithValue("@category", "Risk");
            riskAdapter.Fill(dashboardData);
            riskAdapter.Dispose();

            List<Dictionary<string, object>> allData = utilityManager.GetDataAsDynamic(dashboardData.Tables[0].Rows);
            Dictionary<string, Dictionary<string, List<object>>> categorizedData = utilityManager.CategorizeData(allData);

            return utilityManager.GetRiskData(categorizedData);
        }


        public Dictionary<string, object> GetIsoDetail(string isoCode)
        {
            string isoDetailQuery = "SELECT * FROM ISO_MASTER WHERE ISO_CODE = @isoCode";
            DataSet isoData = new DataSet();
            SqlDataAdapter isoAdapter = new SqlDataAdapter(isoDetailQuery, conn);
            isoAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", isoCode);
            isoAdapter.Fill(isoData);
            isoAdapter.Dispose();
            return utilityManager.GetDataAsDynamic(isoData);
            
        }

        public Dictionary<string, object> GetMerchantDetail(string mid)
        {
            string isoDetailQuery = "SELECT * FROM ITS_Merchant WHERE mm_cust_no = @mid";
            DataSet data = new DataSet();
            SqlDataAdapter isoAdapter = new SqlDataAdapter(isoDetailQuery, conn);
            isoAdapter.SelectCommand.Parameters.AddWithValue("@mid", mid);
            isoAdapter.Fill(data);
            isoAdapter.Dispose();
            return utilityManager.GetDataAsDynamic(data);

        }

        public List<Dictionary<string, object>> GetMerchantReserve(string orgType, string orgCode)
        {
            string isoPart = " ";
            if(orgType == "ISO" && orgCode != "0001")
            {
                isoPart = " AND i.iso_code = @isoCode";
            }else if(orgType == "MCT")
            {
                isoPart = " AND i.mm_cust_no = @mid";

            }
            string reserveQuery = "select h.hb_merch_no, i.mm_legal_name, i.mm_dba_name, MAX(h.hb_reserve) hb_reserve, MAX(h.hb_Percentage) hb_Percentage, MAX(h.hb_beg_bal) hb_beg_bal " + 
                    " FROM ITS_merchant i " +
                    " INNER JOIN HoldBack_Reserve h ON h.hb_merch_no = i.mm_cust_no " +
                    " WHERE h.AutoIdent IN(SELECT MAX(h1.AutoIdent) FROM HoldBack_Reserve h1 WHERE h.hb_merch_no = h1.hb_merch_no) " + isoPart + 
                    " GROUP BY h.hb_merch_no, i.mm_legal_name, i.mm_dba_name " + 
                    " ORDER BY mm_dba_name";
            
            DataSet data = new DataSet();
            SqlDataAdapter reserveAdapter = new SqlDataAdapter(reserveQuery, conn);
            if (orgType == "ISO" && orgCode != "0001")
            {
                reserveAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", orgCode);
            }else if(orgType == "MCT")
            {
                reserveAdapter.SelectCommand.Parameters.AddWithValue("@mid", orgCode);

            }
            reserveAdapter.Fill(data);
            reserveAdapter.Dispose();
            return utilityManager.GetDataAsDynamic(data.Tables[0].Rows);

        }



        

    }
}
