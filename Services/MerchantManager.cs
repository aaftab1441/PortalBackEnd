using System;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using EaglePortal.Models;

namespace EaglePortal.Services
{
    public class MerchantManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public MerchantManager(String connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~MerchantManager()
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
        public Dictionary<string, object> SaveMerchant(Merchant merchant)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand addCommand = conn.CreateCommand();
            #region sql
            string insertSQL = "INSERT INTO Merchant (legalname, legaladdress, legalcity, legalstate, legalzipcode, legalphone, legalfax, " +
                "website, federalid, emailaddress, locationnumber, legalbankruptcy, legalbankruptcyexplain, created_at, updated_at, howlongdba, " +
                "reference2phone, reference2fax, reference2address, reference2account, reference2name, reference1phone, reference1fax, reference1address, " +
                "reference1account, reference1title, reference2title, reference1name, legalzipcodeplus4, stateofincorporation, dateofincorporation, bankruptcydischargedate) " +

                "VALUES(@legalname, @legaladdress, @legalcity, @legalstate, @legalzipcode, @legalphone, @legalfax, @website, @federalid, @emailaddress, @locationnumber, " +
                "@legalbankruptcy, @legalbankruptcyexplain, @created_at, @updated_at, @howlongdba, @reference2phone, @reference2fax, @reference2address, @reference2account, " +
                "@reference2name, @reference1phone, @reference1fax, @reference1address, @reference1account, @reference1title, @reference2title, @reference1name, " +
                "@legalzipcodeplus4, @stateofincorporation, @dateofincorporation, @bankruptcydischargedate) SELECT CAST(scope_identity() AS int);";


            addCommand.CommandText = insertSQL;
            addCommand.Parameters.AddWithValue("@legalname ", merchant.legalname);
            addCommand.Parameters.AddWithValue("@legaladdress ", merchant.legaladdress);
            addCommand.Parameters.AddWithValue("@legalcity ", merchant.legalcity);
            addCommand.Parameters.AddWithValue("@legalstate ", merchant.legalstate);
            addCommand.Parameters.AddWithValue("@legalzipcode ", merchant.legalzipcode);
            addCommand.Parameters.AddWithValue("@legalphone ", merchant.legalphone);
            addCommand.Parameters.AddWithValue("@legalfax ", merchant.legalfax);
            addCommand.Parameters.AddWithValue("@website ", merchant.website);
            addCommand.Parameters.AddWithValue("@federalid ", merchant.federalid);
            addCommand.Parameters.AddWithValue("@emailaddress ", merchant.emailaddress);
            addCommand.Parameters.AddWithValue("@locationnumber ", merchant.locationnumber);
            addCommand.Parameters.AddWithValue("@legalbankruptcy ", merchant.legalbankruptcy);
            addCommand.Parameters.AddWithValue("@legalbankruptcyexplain ", merchant.legalbankruptcyexplain);
            addCommand.Parameters.AddWithValue("@created_at", DateTime.Now);
            addCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
            addCommand.Parameters.AddWithValue("@howlongdba ", merchant.howlongdba);
            addCommand.Parameters.AddWithValue("@reference2phone ", merchant.reference2phone);
            addCommand.Parameters.AddWithValue("@reference2fax ", merchant.reference2fax);
            addCommand.Parameters.AddWithValue("@reference2address ", merchant.reference2address);
            addCommand.Parameters.AddWithValue("@reference2account ", merchant.reference2account);
            addCommand.Parameters.AddWithValue("@reference2name ", merchant.reference2name);
            addCommand.Parameters.AddWithValue("@reference1phone ", merchant.reference1phone);
            addCommand.Parameters.AddWithValue("@reference1fax ", merchant.reference1fax);
            addCommand.Parameters.AddWithValue("@reference1address ", merchant.reference1address);
            addCommand.Parameters.AddWithValue("@reference1account ", merchant.reference1account);
            addCommand.Parameters.AddWithValue("@reference1title ", merchant.reference1title);
            addCommand.Parameters.AddWithValue("@reference2title ", merchant.reference2title);
            addCommand.Parameters.AddWithValue("@reference1name ", merchant.reference1name);
            addCommand.Parameters.AddWithValue("@legalzipcodeplus4 ", merchant.legalzipcodeplus4);
            addCommand.Parameters.AddWithValue("@stateofincorporation ", merchant.stateofincorporation);
            addCommand.Parameters.AddWithValue("@dateofincorporation ", merchant.dateofincorporation);
            addCommand.Parameters.AddWithValue("@bankruptcydischargedate", merchant.bankruptcydischargedate);
            #endregion
            int id = (int)addCommand.ExecuteScalar();

            toReturn.Add("Success", id);

            return toReturn;
        }
        public Dictionary<string, object> GetMerchant(int Id)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand selectCommand = conn.CreateCommand();
            #region sql
            string selectSQL = "SELECT * FROM Merchant WHERE Id = @Id";

            selectCommand.CommandText = selectSQL;
            selectCommand.Parameters.AddWithValue("@Id", Id);

            #endregion
            int ID = (int)selectCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }
        public Dictionary<string, object> UpdateMerchant(Merchant merchant)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand updateCommand = conn.CreateCommand();
            #region sql
            string updateSQL = "UPDATE Merchant SET legalname = @legalname, legaladdress = @legaladdress, legalcity = @legalcity, legalstate = @legalstate, " +
                "legalzipcode = @legalzipcode, legalphone = @legalphone, legalfax = @legalfax, website = @website, federalid = @federalid, emailaddress = @emailaddress, " +
                "locationnumber = @locationnumber, legalbankruptcy = @legalbankruptcy, legalbankruptcyexplain = @legalbankruptcyexplain, updated_at = @updated_at, " +
                "howlongdba = @howlongdba, reference2phone = @reference2phone, reference2fax = @reference2fax, reference2address = @reference2address, " +
                "reference2account = @reference2account, reference2name = @reference2name, reference1phone = @reference1phone, reference1fax = @reference1fax," +
                "reference1address = @reference1address, reference1account = @reference1account, reference1title = @reference1title,reference2title = @reference2title," +
                "reference1name = @reference1name, legalzipcodeplus4 = @legalzipcodeplus4, stateofincorporation = @stateofincorporation, " +
                "dateofincorporation = @dateofincorporation, bankruptcydischargedate = @bankruptcydischargedate WHERE ID =  @Id";


            updateCommand.CommandText = updateSQL;
            updateCommand.Parameters.AddWithValue("@Id", merchant.id);
            updateCommand.Parameters.AddWithValue("@legalname ", merchant.legalname);
            updateCommand.Parameters.AddWithValue("@legaladdress ", merchant.legaladdress);
            updateCommand.Parameters.AddWithValue("@legalcity ", merchant.legalcity);
            updateCommand.Parameters.AddWithValue("@legalstate ", merchant.legalstate);
            updateCommand.Parameters.AddWithValue("@legalzipcode ", merchant.legalzipcode);
            updateCommand.Parameters.AddWithValue("@legalphone ", merchant.legalphone);
            updateCommand.Parameters.AddWithValue("@legalfax ", merchant.legalfax);
            updateCommand.Parameters.AddWithValue("@website ", merchant.website);
            updateCommand.Parameters.AddWithValue("@federalid ", merchant.federalid);
            updateCommand.Parameters.AddWithValue("@emailaddress ", merchant.emailaddress);
            updateCommand.Parameters.AddWithValue("@locationnumber ", merchant.locationnumber);
            updateCommand.Parameters.AddWithValue("@legalbankruptcy ", merchant.legalbankruptcy);
            updateCommand.Parameters.AddWithValue("@legalbankruptcyexplain ", merchant.legalbankruptcyexplain);
            updateCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
            updateCommand.Parameters.AddWithValue("@howlongdba ", merchant.howlongdba);
            updateCommand.Parameters.AddWithValue("@reference2phone ", merchant.reference2phone);
            updateCommand.Parameters.AddWithValue("@reference2fax ", merchant.reference2fax);
            updateCommand.Parameters.AddWithValue("@reference2address ", merchant.reference2address);
            updateCommand.Parameters.AddWithValue("@reference2account ", merchant.reference2account);
            updateCommand.Parameters.AddWithValue("@reference2name ", merchant.reference2name);
            updateCommand.Parameters.AddWithValue("@reference1phone ", merchant.reference1phone);
            updateCommand.Parameters.AddWithValue("@reference1fax ", merchant.reference1fax);
            updateCommand.Parameters.AddWithValue("@reference1address ", merchant.reference1address);
            updateCommand.Parameters.AddWithValue("@reference1account ", merchant.reference1account);
            updateCommand.Parameters.AddWithValue("@reference1title ", merchant.reference1title);
            updateCommand.Parameters.AddWithValue("@reference2title ", merchant.reference2title);
            updateCommand.Parameters.AddWithValue("@reference1name ", merchant.reference1name);
            updateCommand.Parameters.AddWithValue("@legalzipcodeplus4 ", merchant.legalzipcodeplus4);
            updateCommand.Parameters.AddWithValue("@stateofincorporation ", merchant.stateofincorporation);
            updateCommand.Parameters.AddWithValue("@dateofincorporation ", merchant.dateofincorporation);
            updateCommand.Parameters.AddWithValue("@bankruptcydischargedate", merchant.bankruptcydischargedate);

            #endregion
            updateCommand.ExecuteScalar();

            toReturn.Add("Success", merchant.id);

            return toReturn;
        }

        public Dictionary<string, object> UpdateIsoInformation(IsoInformation isoInformation)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand addCommand = conn.CreateCommand();
            #region sql
            string insertSQL = "UPDATE Merchant SET iso_code = @iso_code, sub_iso_code=@sub_iso_code, sales_office=@sales_office, " +
                               "sales_rep_code=@sales_rep_code WHERE id =  @merchantId";


            addCommand.CommandText = insertSQL;

            addCommand.Parameters.AddWithValue("@iso_code", isoInformation.iso_code);
            addCommand.Parameters.AddWithValue("@sub_iso_code", isoInformation.sub_iso_code);
            addCommand.Parameters.AddWithValue("@sales_office", isoInformation.sales_office);
            addCommand.Parameters.AddWithValue("@sales_rep_code", isoInformation.sales_rep_code);
            addCommand.Parameters.AddWithValue("@merchantId", isoInformation.id);
            #endregion
            addCommand.ExecuteScalar();

            toReturn.Add("Success", isoInformation.id);

            return toReturn;
        }
        public Dictionary<string, object> GetISOInfo(int Id)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand selectCommand = conn.CreateCommand();
            #region sql
            string selectSQL = "SELECT iso_code, sub_iso_code, sales_office, sales_rep_code FROM Merchant WHERE id = @merchantId";

            selectCommand.CommandText = selectSQL;
            selectCommand.Parameters.AddWithValue("@merchantId", Id);

            #endregion
            int ID = (int)selectCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }

        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}