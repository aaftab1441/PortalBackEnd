using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EaglePortal.Models
{
    public class Merchant
    {
        public int id { get; set; }
        public string legalname { get; set; }
        public string legaladdress { get; set; }
        public string legalcity { get; set; }
        public string legalstate { get; set; }
        public string legalzipcode { get; set; }
        public string legalzipcodeplus4 { get; set; }
        public string legalphone { get; set; }
        public string legalfax { get; set; }
        public string emailaddress { get; set; }
        public string website { get; set; }
        public string federalid { get; set; }
        public string stateofincorporation { get; set; }
        public string dateofincorporation { get; set; }
        public string howlongdba { get; set; }
        public string ownershiptype { get; set; }
        public string mccsubtype { get; set; }
        public string locationnumber { get; set; }
        public string legalbankruptcy { get; set; }
        public string legalbankruptcyexplain { get; set; }
        public string bankruptcydischargedate { get; set; }
        public string reference1name { get; set; }
        public string reference2name { get; set; }
        public string reference1address { get; set; }
        public string reference2address { get; set; }
        public string reference1companyname { get; set; }
        public string reference2companyname { get; set; }
        public string reference1title { get; set; }
        public string reference2title { get; set; }
        public string reference1phone { get; set; }
        public string reference2phone { get; set; }
        public string reference1fax { get; set; }
        public string reference2fax { get; set; }
        public string reference1account { get; set; }
        public string reference2account { get; set; }
    }
}
