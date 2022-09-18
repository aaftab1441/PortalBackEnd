using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;

namespace EaglePortal.Services
{
    public class ConfigManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public ConfigManager(string connectionString) {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }

        ~ConfigManager()
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
        public Hashtable ListConfig() {
            Hashtable toReturn = new Hashtable();
            string previousName;
            List<dynamic> currentConfig = new List<dynamic>();
            SqlDataAdapter configAdapter = new SqlDataAdapter("SELECT * from Config_table order by name, code", conn);
            SqlDataAdapter sicAdapter = new SqlDataAdapter("SELECT sic_code, sic_desc from sic_file order by sic_code, sic_desc", conn);

            SqlDataAdapter isoAdapter = new SqlDataAdapter("SELECT ISO_CODE as code, ISO_NAME as value from ISO_MASTER  order by ISO_NAME", conn);
            SqlDataAdapter subIsoAdapter = new SqlDataAdapter("SELECT Code as code, Name  as value FROM Sub_Iso  order by Name", conn);
            SqlDataAdapter salesOfficeAdapter = new SqlDataAdapter("SELECT Code as code, Name  as value FROM Sales_Office  order by Name", conn);
            SqlDataAdapter salesAgentAdapter = new SqlDataAdapter("SELECT Code as code, CONCAT(First_Name, ' ' , Last_Name) as value FROM Sales_Agent  order by Last_Name", conn);

            SqlDataAdapter isoIdAdapter = new SqlDataAdapter("SELECT AutoIdent as code, ISO_NAME as value from ISO_MASTER  order by ISO_NAME", conn);
            SqlDataAdapter subIsoIdAdapter = new SqlDataAdapter("SELECT AutoIdent as code, Name  as value FROM Sub_Iso  order by Name", conn);
            SqlDataAdapter salesOfficeIdAdapter = new SqlDataAdapter("SELECT AutoIdent as code, Name  as value FROM Sales_Office  order by Name", conn);
            SqlDataAdapter salesAgentIdAdapter = new SqlDataAdapter("SELECT AutoIdent as code, CONCAT(First_Name, ' ' , Last_Name) as value FROM Sales_Agent  order by Last_Name", conn);

            DataSet results = new DataSet();
            DataSet sicResults = new DataSet();
            DataSet isoResults = new DataSet();
            DataSet subISOResults = new DataSet();
            DataSet salesOfficeResults = new DataSet();
            DataSet salesAgentResults = new DataSet();

            DataSet isoIdResults = new DataSet();
            DataSet subISOIdResults = new DataSet();
            DataSet salesOfficeIdResults = new DataSet();
            DataSet salesAgentIdResults = new DataSet();

            configAdapter.Fill(results);
            previousName = "";

            foreach (DataRow aRow in results.Tables[0].Rows) { 
                if(previousName != (string) aRow["Name"] && currentConfig.Count > 0){
                    toReturn.Add(previousName, currentConfig);
                    currentConfig = new List<dynamic>();
                }
                previousName = (string) aRow["Name"];
                currentConfig.Add(utilityManager.GetDataAsDynamic(aRow));
            }

            if(!toReturn.ContainsKey(previousName)){
                toReturn.Add(previousName, currentConfig);
            }

            sicAdapter.Fill(sicResults);
            currentConfig = new List<dynamic>();
            foreach (DataRow aRow in sicResults.Tables[0].Rows) {
                currentConfig.Add(utilityManager.GetDataAsDynamic(aRow));
            }
            toReturn.Add("SIC_CODES", currentConfig);

            isoAdapter.Fill(isoResults);
            subIsoAdapter.Fill(subISOResults);
            salesOfficeAdapter.Fill(salesOfficeResults);
            salesAgentAdapter.Fill(salesAgentResults);

            toReturn.Add("ISO_LIST", ResultsToList(isoResults));
            toReturn.Add("SUB_ISO_LIST", ResultsToList(subISOResults));
            toReturn.Add("SALES_OFFICE_LIST", ResultsToList(salesOfficeResults));
            toReturn.Add("SALES_AGENT_LIST", ResultsToList(salesAgentResults));

            
            isoIdAdapter.Fill(isoIdResults);
            subIsoIdAdapter.Fill(subISOIdResults);
            salesOfficeIdAdapter.Fill(salesOfficeIdResults);
            salesAgentIdAdapter.Fill(salesAgentIdResults);

            toReturn.Add("ISO_ID_LIST", ResultsToList(isoIdResults));
            toReturn.Add("SUB_ID_ISO_LIST", ResultsToList(subISOIdResults));
            toReturn.Add("SALES_OFFICE_ID_LIST", ResultsToList(salesOfficeIdResults));
            toReturn.Add("SALES_AGENT_ID_LIST", ResultsToList(salesAgentIdResults));

            return toReturn;
        }

        private List<dynamic> ResultsToList(DataSet results)
        {
            List<dynamic> toReturn = new List<dynamic>();
            foreach (DataRow aRow in results.Tables[0].Rows)
            {
                toReturn.Add(utilityManager.GetDataAsDynamic(aRow));
            }
            return toReturn;
        }
    }
}
