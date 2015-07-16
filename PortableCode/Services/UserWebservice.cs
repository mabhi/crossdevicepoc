using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.Models;

namespace PortableCode.Services
{
    public class UserWebservice
    {
		public User currentUser;
		static readonly UserWebservice instance = new UserWebservice();
        /// <summary>
        /// Gets the instance of the Azure Web Service
        /// </summary>
		private RestClient restClient {get; set;}

		private UserWebservice(){
			restClient = new RestClient (User.BaseURL);
		}
			
        public static UserWebservice Instance
        {
            get
            {
                return instance;
            }
        }


		public async Task<string> AuthenticateWithCredentialsAsync(string username, string password){

			var responseString = await restClient.MakeRequestAsync (User.GetAuthenticateURL(username,password));				
			return responseString;
		}
    }
}
