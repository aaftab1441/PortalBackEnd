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
    public class MerchantListMangaer
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public MerchantListMangaer(string connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();
        }
        ~MerchantListMangaer()
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
        public Hashtable GetMerchantsByStatus(string []parts, JsonElement search)  {
            Hashtable toReturn = new Hashtable();
            string merchantStatus = parts[1];
            SqlDataAdapter merchantListAdapter = new SqlDataAdapter();
            DataSet merchantListDataSet = new DataSet(), isoDataSet = new DataSet();
            string isoCode = "";
            string selectFields = "mm_cust_no, mm_legal_name, mm_dba_name, mm_location_address, mm_location_address_2, mm_location_city, mm_location_state, mm_location_zip, " +
                " mm_contact_phone, mm_status_date, mm_owner_first_1, mm_owner_mi_1, mm_owner_last_1";
            if(parts[0] == "main") {
               merchantListAdapter = new SqlDataAdapter("select " + selectFields + " FROM [ITS_merchant]  i WHERE mm_status_code LIKE @st AND mm_legal_name like @legalName AND " + 
                " mm_dba_name like @dbaName AND mm_cust_no like @mid AND mm_owner_last_1 like @ownerLastName", conn);
            } else if (parts[0] == "iso") {
               merchantListAdapter = new SqlDataAdapter("select " + selectFields + " FROM [ITS_merchant]  i WHERE i.iso_code = @isoCode  AND mm_status_code LIKE @st  " + 
                   " AND mm_legal_name like @legalName AND mm_dba_name like @dbaName AND mm_cust_no like @mid AND mm_owner_last_1  like @ownerLastName ", conn);
               merchantListAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", parts[2]);
            }

            if(merchantStatus == "all" || merchantStatus == "undefined") {
                merchantStatus = "%";
            } else if (merchantStatus == "active") {
               merchantStatus = "A";
            } else if (merchantStatus == "closed") {
               merchantStatus = "C";
            } else if (merchantStatus == "inactive") {
               merchantStatus = "S";
            }

            merchantListAdapter.SelectCommand.Parameters.AddWithValue("@st", merchantStatus);
            merchantListAdapter.SelectCommand.Parameters.AddWithValue("@legalName", string.Format("%{0}%", search.GetProperty("legalName").GetString()));
            merchantListAdapter.SelectCommand.Parameters.AddWithValue("@dbaName", string.Format("%{0}%", search.GetProperty("dbaName").GetString()));
            merchantListAdapter.SelectCommand.Parameters.AddWithValue("@mid", string.Format("%{0}%", search.GetProperty("mid").GetString()));
            merchantListAdapter.SelectCommand.Parameters.AddWithValue("@ownerLastName", string.Format("%{0}%", search.GetProperty("ownerLastName").GetString()));

            merchantListAdapter.Fill(merchantListDataSet);
            merchantListAdapter.Dispose();
            if(parts[0] == "iso") {
               merchantListAdapter = new SqlDataAdapter("select * FROM [ISO_MASTER ] WHERE ISO_CODE = @isoCode ", conn);
                merchantListAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", parts[2]);
                merchantListAdapter.Fill(isoDataSet);
                merchantListAdapter.Dispose();
                toReturn.Add("ISO", utilityManager.GetDataAsDynamic(isoDataSet.Tables[0].Rows));
                toReturn.Add("MerchantStatus", merchantStatus);
                isoCode = parts[2];
            }

            toReturn.Add("TotalCount", GetMerchantCounts(isoCode, "%"));
            toReturn.Add("ActiveCount", GetMerchantCounts(isoCode, "A"));
            toReturn.Add("ClosedCount", GetMerchantCounts(isoCode, "C"));
            toReturn.Add("InactiveCount", GetMerchantCounts(isoCode, "S"));

            toReturn.Add("Data", utilityManager.GetDataAsDynamic(merchantListDataSet.Tables[0].Rows));
            return toReturn;
        }

        public int GetMerchantCounts(string isoCode, string status) {
            SqlDataAdapter merchantCountAdapter = new SqlDataAdapter();
            DataSet merchantCountDataSet = new DataSet();
            int count = 0;
            if(isoCode.Length > 0) {
               merchantCountAdapter = new SqlDataAdapter("select Count(*) Merchant_Count FROM [ITS_merchant] WHERE  mm_status_code LIKE @st AND iso_code  = @isoCode ", conn);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@st", status);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@isoCode", isoCode);
            } else {
                merchantCountAdapter = new SqlDataAdapter("select Count(*) Merchant_Count FROM [ITS_merchant] WHERE  mm_status_code like @st ", conn);
                merchantCountAdapter.SelectCommand.Parameters.AddWithValue("@st", status);

            }
            merchantCountAdapter.Fill(merchantCountDataSet);
            merchantCountAdapter.Dispose();
            count = (int) utilityManager.GetFirstValue(merchantCountDataSet.Tables[0].Rows, "Merchant_Count");
            return count;
        }
    }
}
