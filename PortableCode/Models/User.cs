using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using PortableCode.BusinessLayer.Contracts;

namespace PortableCode.Models
{
    public class User : BusinessEntityBase
    {

        [JsonProperty(PropertyName = "Id")]
        public string UserId { get; set; }

        [JsonProperty/*(PropertyName = "Password")*/]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "TeritorryId")]
        public int TerritoryId { get; set; }

        public User()
        {
            UserId = string.Empty;
            Password = string.Empty;
            TerritoryId = int.MinValue;
        }

        public User(User anUser)
        {
          SyncProperties(anUser);
        }

        public void SyncProperties(User anUser)
        {
          this.UserId = anUser.UserId;
          this.Password = anUser.Password;
          this.TerritoryId = anUser.TerritoryId;
        }

        public bool IsDirty { get; set; }

		/**
		 * This method returns the authenticate string component of the API.
		 * Parameters- username : The user id used for authenticating and Password: secret key.
		 */ 
		public static string GetAuthenticateURL (string username, string password)
		{
			return String.Format("api/users/{0}/{1}",username,password);
		}


    }
}
