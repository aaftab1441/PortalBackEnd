using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EaglePortal.Models
{
    public class CriteriaCollection : ArrayList
    {
        public CriteriaCollection() : base()
        {

        }

        public void addCondition(string searchField, string field, string sign, object value)
        {
            if(value != null)
            {
                if(value.ToString().Length > 0)
                {
                    Hashtable aCondition = new Hashtable();
                    aCondition.Add("SearchField", searchField);
                    aCondition.Add("Field", field);
                    aCondition.Add("Sign", sign);
                    aCondition.Add("Value", value);
                    this.Add(aCondition);

                }

            }
        }

        public string getWhere()
        {
            ArrayList whereList = new ArrayList();
            Hashtable aCondition;
            for (int i = 0; i < this.Count; i++)
            {
                aCondition = (Hashtable)this[i];
                whereList.Add(aCondition["SearchField"] + " " + aCondition["Sign"] + " " + "@" + aCondition["Field"]);
            }
            Array where = whereList.ToArray();
            string toReturn = string.Join(" AND ", (string[])whereList.ToArray(Type.GetType("System.String")));
            return toReturn;
        }

        public string formatDate(object aDate, string dateFormat)
        {
            string dateValue = null;
            if (aDate != null )
            {
                dateValue = aDate.ToString();
                if(dateValue.Length > 0)
                {
                    dateValue = DateTime.Parse(dateValue).ToString(dateFormat);
                }
                

            }else
            {
                dateValue = "";
            }
            return dateValue;
        }
        public SqlParameterCollection addParameterValues(SqlParameterCollection parameterCollection)
        {
            Hashtable aCondition;
            for (int i = 0; i < this.Count; i++)
            {
                aCondition = (Hashtable)this[i];
                parameterCollection.AddWithValue("@" + aCondition["Field"], aCondition["Value"]);
            }
            return parameterCollection;
        }
    }
}
