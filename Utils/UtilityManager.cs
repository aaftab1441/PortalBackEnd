using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using System.Threading.Tasks;
using System.Collections;
using System.Text.Json;

namespace EaglePortal.Utils
{
    public class UtilityManager
    {
        public Object GetFirstValue(DataRowCollection data, String columnName) {
            Object toReturn = null;
            if (data.Count > 0) {
                toReturn = data[0][columnName];
            }
            return toReturn;
        }

        public Hashtable addToHashtable(Dictionary<string, object> toAdd, Hashtable addTo)
        {
            foreach(KeyValuePair<string, object> e in toAdd)
            {
                addTo.Add(e.Key, e.Value);
            }
            return addTo;
        }

        public Hashtable addToHashtable(Hashtable toAdd, Hashtable addTo)
        {
            foreach (DictionaryEntry e in toAdd)
            {
                addTo.Add(e.Key, e.Value);
            }
            return addTo;
        }

        public List<Dictionary<string, object>> processDateConversion(List<Dictionary<string, object>> theData, Boolean isPrevious)
        {
            string aDate, year;
            dynamic anObject;

            for (int i = 0; i < theData.Count; i++)
            {
                anObject = theData.ElementAt(i);

                aDate = "" + anObject["x"];
                if (isPrevious)
                {
                    if (aDate.Length == 6)
                    {
                        year = aDate.Substring(0, 2);
                        year = "20" + year;
                        anObject["x"] = (Int32.Parse(year) + 1).ToString() + aDate.Substring(2, 4);
                    }
                    else if (aDate.Length == 8)
                    {
                        year = aDate.Substring(0, 4);
                        anObject["x"] = (Int32.Parse(year) + 1).ToString() + aDate.Substring(4, 4);

                    }
                }
                else
                {
                    if (aDate.Length == 6)
                    {
                        year = aDate.Substring(0, 2);
                        year = "20" + year;
                        anObject["x"] = year + aDate.Substring(2, 4);
                    }
                    else if (aDate.Length == 8)
                    {
                        year = aDate.Substring(0, 4);
                        anObject["x"] = year + aDate.Substring(4, 4);
                    }

                }
                theData[i] = anObject;
            }


            return theData;
        }

        public string GetValue(Dictionary<string, Dictionary<string, List<object>>> allData, String category, string variable)
        {
            string toReturn = "";
            Dictionary<string, List<object>> categoryContainer;
            List<object> variables;
            Dictionary<string, object> aRow;
            Dictionary<string, object> returnRow;
            if (allData.ContainsKey(category))
            {
                categoryContainer = allData[category];
                if (categoryContainer.ContainsKey(variable))
                {
                    variables = categoryContainer[variable];
                    if (variables.Count > 0)
                    {
                        toReturn = (string)((Dictionary<string, object>)variables[0])["value"];
                    }

                }
            }
            return toReturn;
        }

        public List<Dictionary<string, object>> GetChartData(Dictionary<string, Dictionary<string, List<object>>> allData, String category, string variable)
        {
            List<Dictionary<string, object>> toReturn = new List<Dictionary<string, object>>();
            Dictionary<string, List<object>> categoryContainer;
            List<object> variables;
            Dictionary<string, object> aRow;
            Dictionary<string, object> returnRow;
            if (allData.ContainsKey(category))
            {
                categoryContainer = allData[category];
                if (categoryContainer.ContainsKey(variable))
                {
                    variables = categoryContainer[variable];
                    for (int i = 0; i < variables.Count; i++)
                    {
                        aRow = (Dictionary<string, object>)variables[i];
                        returnRow = new Dictionary<string, object>();
                        returnRow.Add("y", aRow["value"]);
                        returnRow.Add("x", aRow["period"]);
                        toReturn.Add(returnRow);

                    }
                }
            }
            return toReturn;
        }

        public Dictionary<string, Dictionary<string, List<object>>> CategorizeData(List<Dictionary<string, object>> allData)
        {
            Dictionary<string, Dictionary<string, List<object>>> toReturn = new Dictionary<string, Dictionary<string, List<object>>>();
            string previousCategory = "", previousVariable = "";
            string currentCategory = "", currentVariable = "";
            Dictionary<string, List<object>> categoryContainer = new Dictionary<string, List<object>>();
            //            Dictionary<string, object> variableContainer = new Dictionary<string, object>();
            List<object> variableContainer = new List<object>();

            for (int i = 0; i < allData.Count; i++)
            {
                currentCategory = (string)allData[i]["category"];
                currentVariable = (string)allData[i]["variable_name"];

                if (!toReturn.ContainsKey(currentCategory))
                {
                    toReturn.Add(currentCategory, new Dictionary<string, List<object>>());
                }
                categoryContainer = toReturn[currentCategory];
                if (!categoryContainer.ContainsKey(currentVariable))
                {
                    categoryContainer.Add(currentVariable, new List<object>());
                }
                variableContainer = categoryContainer[currentVariable];
                variableContainer.Add(allData[i]);

            }

            return toReturn;
        }




        public string getParentIdField(string parentType)
        {
            string parentId = "";
            if(parentType == "DAS" || parentType == "ISO")
            {
                parentId = "iso_id";
            }else if(parentType == "SUB-ISO")
            {
                parentId = "sub_iso_id";
            }
            else if (parentType == "SALES-OFFICE")
            {
                parentId = "sales_office_id";
            }
            else if (parentType == "SALES-AGENT")
            {
                parentId = "sales_office_id";
            }
            return parentId;
        }
        public string TryGetProperty(JsonElement element, string value)
        {
            string toReturn = "";
            JsonElement jsonToReturn;
            try
            {
                if (element.TryGetProperty(value, out jsonToReturn))
                {
                    toReturn = jsonToReturn.GetString();
                }

            }
            catch (Exception e)
            {

            }
            return toReturn;
        }
        public Hashtable GetHashMap(JsonElement element)
        {
            Hashtable temp = JsonSerializer.Deserialize<Hashtable>(element.ToString());
            Hashtable toReturn = new Hashtable();
            foreach (DictionaryEntry pair in temp)
            {
                toReturn.Add(pair.Key, ((JsonElement)pair.Value).ToString());
            }
            return toReturn;
        }
        public List<Dictionary<string, object>> GetDataAsDynamic(DataRowCollection data) {
            Dictionary<string, object> aRow;
            List<Dictionary<string, object>> rowCollection = new List<Dictionary<string, object>>();

            foreach (DataRow row in data) {
                aRow = new Dictionary<string, Object>();
                foreach (DataColumn col in row.Table.Columns){
                    aRow.Add(col.ColumnName, "" + row[col.ColumnName]);
                }
                rowCollection.Add(aRow);
            }
            return rowCollection;
        }

        public Dictionary<string, object> GetDataAsDynamic(DataRow row) {
            Dictionary<string, object> aRow = new Dictionary<string, Object>();
            foreach (DataColumn col in row.Table.Columns) {
                aRow.Add(col.ColumnName, "" + row[col.ColumnName]);
            }
            return aRow;
        }
        public Dictionary<string, object> GetFirstRowDataAsDynamic(DataRow row)
        {
            Dictionary<string, object> aRow = null;
            if(row != null)
            {
                aRow = new Dictionary<string, Object>();
                foreach (DataColumn col in row.Table.Columns)
                {
                    aRow.Add(col.ColumnName, "" + row[col.ColumnName]);
                }

            }
            return aRow;
        }

        public Dictionary<string, object> GetDataAsDynamic(DataSet data)
        {
            Dictionary<string, Object> aRow = new Dictionary<string, Object>();
            if(data != null && data.Tables != null && data.Tables.Count > 0 && data.Tables[0].Rows != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                foreach (DataColumn col in row.Table.Columns)
                {
                    aRow.Add(col.ColumnName, "" + row[col.ColumnName]);
                }

            }
            return aRow;
        }

        public List<Dictionary<string, object>> GetRiskData(Dictionary<string, Dictionary<string, List<object>>> allData)
        {
            List<Dictionary<string, object>> toReturn = new List<Dictionary<string, object>>();
            Dictionary<string, List<object>> riskData;
            Dictionary<string, object> aRow;
            string dateKey;
            if (allData.ContainsKey("Risk"))
            {
                riskData = allData["Risk"];
                List<Dictionary<string, object>> sales = this.GetChartData(allData, "Risk", "Sales");
                List<Dictionary<string, object>> transactionCounts = this.GetChartData(allData, "Risk", "Transaction_Count");
                List<Dictionary<string, object>> creditSales = this.GetChartData(allData, "Risk", "Credit_Sales");
                List<Dictionary<string, object>> creditTransactionCount = this.GetChartData(allData, "Risk", "Credit_Transaction_Count");
                List<Dictionary<string, object>> chargeBackCount = this.GetChartData(allData, "Risk", "ChargeBack_Count");
                List<Dictionary<string, object>> chargeBackAmount = this.GetChartData(allData, "Risk", "ChargeBack_Amount");
                List<Dictionary<string, object>> swipeCount = this.GetChartData(allData, "Risk", "Swipe_Count");
                List<Dictionary<string, object>> manualCount = this.GetChartData(allData, "Risk", "Manual_Count");

                for (int i = 0; i < sales.Count; i++)
                {
                    aRow = new Dictionary<string, object>();
                    dateKey = (string)sales[i]["x"];
                    aRow.Add("Sales", GetRiskValue(sales, i, dateKey));
                    aRow.Add("Transaction_Count", GetRiskValue(transactionCounts, i, dateKey));
                    aRow.Add("Credit_Sales", GetRiskValue(creditSales, i, dateKey));
                    aRow.Add("Credit_Transaction_Count", GetRiskValue(creditTransactionCount, i, dateKey));
                    aRow.Add("ChargeBack_Count", GetRiskValue(chargeBackCount, i, dateKey));
                    aRow.Add("ChargeBack_Amount", GetRiskValue(chargeBackAmount, i, dateKey));
                    aRow.Add("Swipe_Count", GetRiskValue(swipeCount, i, dateKey));
                    aRow.Add("Manual_Count", GetRiskValue(manualCount, i, dateKey));
                    aRow.Add("Date", dateKey);
                    toReturn.Add(aRow);
                    Console.WriteLine(dateKey);
                }
            }

            return toReturn;
        }


        private string GetRiskValue(List<Dictionary<string, object>> dataContainer, int index, string dateKey)
        {
            string toReturn = "0", aDate;
            if (dataContainer != null && dataContainer.Count > index)
            {
                if (dataContainer[index].ContainsKey("x"))
                {
                    aDate = (string)dataContainer[index]["x"];
                    if (aDate.Length == 6 && aDate == dateKey)
                    {
                        toReturn = (string)dataContainer[index]["y"];
                    }
                    else if (aDate.Length == 8 && aDate == "20" + dateKey)
                    {
                        toReturn = (string)dataContainer[index]["y"];
                    }
                }
            }
            return toReturn;
        }



    }
}
