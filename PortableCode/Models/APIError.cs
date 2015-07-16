using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PortableCode.Models
{
    public class APIError
    {
        [JsonProperty(PropertyName = "Message")]
        public string errorMessage {get;set;}

        public APIError()
        {
            errorMessage = string.Empty;
        }
    }
}
