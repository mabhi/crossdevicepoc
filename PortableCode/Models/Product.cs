using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.BusinessLayer.Contracts;
using Newtonsoft.Json;

namespace PortableCode.Models
{
    public class Product : BusinessEntityBase
    {
         [JsonProperty(PropertyName = "Id")]
        public int ProductId { get; set; }

        [JsonProperty]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public float ProductPrice { get; set; }

        [JsonProperty]
        public int CountryId { get; set; }

        public Product()
        {
            ProductId = int.MinValue;
            ProductName = string.Empty;
            ProductPrice = int.MinValue;
            CountryId = int.MinValue;
        }

        public static string GetProductsInCountryURL(int countryId)
        {
            return String.Format("api/Products/Countries/{0}/Products", countryId.ToString());
        }
    }
    
}
