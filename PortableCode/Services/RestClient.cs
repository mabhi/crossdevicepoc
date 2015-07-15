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


namespace RestTest
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
        
        public RestClient() : this("")
        {
            
        }
        public RestClient(string endpoint) : this(endpoint,HttpVerb.GET)
        {
            
        }

        public RestClient(string endpoint, HttpVerb method): this(endpoint,method,"")
        {
            
        }

        public RestClient(string endpoint, HttpVerb method, string postData) : this(endpoint,method,postData,ContentTypeIdentifiers.kContentTypeJSON)
        {
        }

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
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

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

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }


    } // class

}
