using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.Models;
using Newtonsoft.Json;
using System.Net;
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


		public void InvalidateCurrentUser(){
			_currentUser = null;
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

        public async Task<OrgUnit> GetParticularOrgUnitAsync(int territoryId)
        {
            String responseAsString = await InvokeWebserviceAtURLAsync(OrgUnit.GetOrgUnitDetailURL(territoryId));
            return JsonConvert.DeserializeObject<OrgUnit>(responseAsString);
        }
		//				"[{\"Id\": 1,\"ProductName\": \"sample string 2\",\"Price\": 3.1,\"CountryId\": 4},{\"Id\": 1,\"ProductName\": \"sample string 2\",\"Price\": 3.1,\"CountryId\": 4}]";
		/**
		 *  This asyncronous method gets the list of all products under a territory following indirection => 
		 * territory.Parent Id -> Area.Parent Id -> Country.
		 * Parameter : The area code which starts the indirection.
		 * 
		 */
		public async Task<List<Product>> GetProductsForAreaAsync(int areaCode){
			OrgUnit Area = await GetParticularOrgUnitAsync (areaCode);
			return await GetProductsForCountryAsync(Area.ParentId);
		}

		/**
		 * This gets us the list of product available for a country. It takes the country code and determines the products enlisted
		 * in the web service.
		 * parameter : Country code whose products are asked for.
		 */ 
		public async Task<List<Product>> GetProductsForCountryAsync(int countryCode){
			String responseAsString = await InvokeWebserviceAtURLAsync (Product.GetProductsInCountryURL (countryCode));
			return JsonConvert.DeserializeObject<List<Product>> (responseAsString);
		}
        
		/**
		 * Actual private method that fires up the web serivce client as tasks. This invokes the rest -client previously configured
		 * to return the output as resonse string from web serivce request. It throws any exception it encounters during task running.
		 * 
		 */ 
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
			catch (WebException ex){
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
