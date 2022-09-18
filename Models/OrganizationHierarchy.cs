using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EaglePortal.Models
{
    public class OrganizationHierarchy
    {
        public string parentType, parentFieldId, parentField, parentTable, tableCode, nextParent;
        public OrganizationHierarchy()
        {
            parentType = "";
            parentFieldId = "";
            parentField = "";
            parentTable = "";
            tableCode = "";
            nextParent = "";
        }
        public OrganizationHierarchy(string aParentType, string aParentTable, string aParentField, string aParentFieldId, string aTableCode, string aNextParent)
        {
            parentType = aParentType;
            parentFieldId = aParentFieldId;
            parentField = aParentField;
            parentTable = aParentTable;
            tableCode = aTableCode;
            nextParent = aNextParent;
        }
    }
}
