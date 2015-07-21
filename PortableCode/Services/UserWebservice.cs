using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.Models;
using Newtonsoft.Json;
using System.Net.Http;
using PortableCode.Exceptions;

namespace PortableCode.Services
{
    public class UserWebservice
    {

        private APIError _anyError;
        private RestClient _restClient;
        private User _currentUser;
		static readonly UserWebservice instance = new UserWebservice();
        /// <summary>
        /// Gets the instance of the Azure Web Service
        /// </summary>
		public RestClient MobileWebClient {
            get
            {
                return _restClient;
            }
        }

        public APIError Error
        {
            get
            {
                return _anyError;
            }
        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }

        private UserWebservice(){
			_restClient = new RestClient (User.BaseURL());
            _currentUser = null;
            _anyError = null;
		}
			
        public static UserWebservice Instance
        {
            get
            {
                return instance;
            }
        }


	

        public async Task<List<Customer>> GetCustomersInTerritoryAsync(int territoryId)
        {
            String responseAsString = await InvokeWebserviceAtURLAsync(Customer.GetCustomersInTerritoryURL(territoryId));
            List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(responseAsString);
            return listOfCustomers;
        }

        public async Task AuthenticateWithCredentialsAsync(string username, string password)
        {
            String responseAsString = await InvokeWebserviceAtURLAsync(User.GetAuthenticateURL(username, password));
            _currentUser = JsonConvert.DeserializeObject<User>(responseAsString);

        }

        private async Task<String> InvokeWebserviceAtURLAsync(string URLString)
        {
             var responseString = string.Empty;
			try
			{
				 responseString = await _restClient.MakeRequestAsync (URLString);
            }
			catch (HttpRequestException ex){
				throw new CustomHttpException (ex.Message);
			}
            catch (JsonException ex)
            {
                _anyError = JsonConvert.DeserializeObject<APIError>(responseString);
            }

            return responseString;
            
        } 
    }
}
