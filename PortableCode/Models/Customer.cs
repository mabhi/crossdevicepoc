using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortableCode.BusinessLayer.Contracts;

namespace PortableCode.Models
{
    public class Customer : BusinessEntityBase
    {

        [JsonProperty(PropertyName = "Id")]
        public int CustomerId { get; set; }

        [JsonProperty (PropertyName = "Name")]
        public string CustomerName { get; set; }

        [JsonProperty(PropertyName = "TeritoryId")]
        public int TerritoryId { get; set; }

        [JsonProperty(PropertyName = "Teritory")]
        public OrgUnit OrgUnitEntity { get; set; }

        public Customer()
        {
            CustomerId = int.MinValue;
            CustomerName = string.Empty;
            TerritoryId = int.MinValue;
        }

        public static string GetCustomersInTerritoryURL(int territoryId)
        {
            return String.Format("territories/{0}/customers", territoryId.ToString());
        }
    }
}
