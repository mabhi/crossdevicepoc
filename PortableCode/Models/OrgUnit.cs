using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.BusinessLayer.Contracts;
using Newtonsoft.Json;

namespace PortableCode.Models
{
    public class OrgUnit : BusinessEntityBase
    {
        [JsonProperty(PropertyName = "Id")]
        public int OrgUnitId { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string OrgName { get; set; }
        [JsonProperty]
        public int ParentId { get; set; }
        [JsonProperty]
        public int OrgTypeId { get; set; }
        public OrgType OrgTypeEntity;

        public OrgUnit()
        {
            OrgUnitId = int.MinValue;
            OrgName = string.Empty;
            ParentId = int.MinValue;
            OrgTypeId = int.MinValue;
        }

        public static string GetAllOrgUnits()
        {
            return "api/OrgUnits";
        }

        public static string GetOrgUnitDetail(int orgUnitId)
        {
            return String.Format("api/OrgUnits/{0}",orgUnitId);
        }
    }
}
