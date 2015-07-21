using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.BusinessLayer.Contracts;
using Newtonsoft.Json;

namespace PortableCode.Models
{
    public class OrgType : BusinessEntityBase
    {
        [JsonProperty(PropertyName = "Id")]
        public int OrgTypeId { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string OrgTypeName{ get; set; }

        public OrgType()
        {
            OrgTypeId = int.MinValue;
            OrgTypeName = String.Empty;
        }

        public static string GetAllOrgTypes()
        {
            return "api/OrgTypes";
        }

        public static string GetOrgTypeDetail(int orgUnitId)
        {
            return String.Format("api/OrgTypes/{0}", orgUnitId);
        }
    }
}
