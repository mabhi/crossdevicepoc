using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}


namespace PortableCode.Services
{
    public static class ContentTypeIdentifiers{
        public const string kContentTypeJSON = "application/json";
        public const string kContentTypeXML = "application/xml";
    }

    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        /**
         * RestClient with no end points mentioned.Default none end-point Note : invalid scenario 
         */
        public RestClient() : this(""){}
        
		/**
         * RestClient with an end point as parameter. Default: Get request type 
         */
        public RestClient(string endpoint) : this(endpoint,HttpVerb.GET){}

		/**
         * RestClient with an end point, Get request type and Default: no post data.
         */
        public RestClient(string endpoint, HttpVerb method): this(endpoint,method,""){}


		/**
         * RestClient with an end point, Get request type and post data. Default Content-Type : applicaton/JSON
         */
        public RestClient(string endpoint, HttpVerb method, string postData) : this(endpoint,method,postData,ContentTypeIdentifiers.kContentTypeJSON){}


		/**
         * RestClient with an end point, request type, post-data if any and content type
         */
        public RestClient (string endpoint, HttpVerb method, string postData,string contentTypeIdentifier)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentTypeIdentifier;
            PostData = postData;
        }

        public async Task<string> MakeRequestAsync()
        {
            return await MakeRequestAsync("");
        }
        
        public async Task<string> MakeRequestAsync(string parameters)
        {

            //Initialize an HttpWebRequest for the current URL
            String URLString = EndPoint + parameters;
            System.Diagnostics.Debug.WriteLine("URL -> {0}", URLString);
            var request = (HttpWebRequest)WebRequest.Create(URLString);
            
            request.Method = Method.ToString();
            request.ContentType = ContentType;
            /*
             //Handle Post request later
            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            */

            //Send the request to the internet resource and wait for the response
            using (WebResponse response = await request.GetResponseAsync())
            /**
             *  The previous statement abbreviates to following two statements.
             *  Task<WebResponse> responseTask = webRequest.GetResponseAsync();
             *  using (WebResponse response = await responseTask)
             */ 
            {
                var responseValue = string.Empty;
                
                if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", ((HttpWebResponse)response).StatusCode);
                    throw new HttpRequestException(message);
                }

				// grab the response,Get a stream representation of the HTTP web response:

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
							responseValue = await reader.ReadToEndAsync();
                        }
                }

                return responseValue;
            }
        }


    } // class

}
