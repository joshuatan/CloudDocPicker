using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace CloudDocPicker.Models.RestApi
{
    public class RestApiClient
    {
        public enum HttpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string AccessToken { get; set; }
        public string Parameters { get; set; }
        public string PostData { get; set; }

        public RestApiClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            PostData = "";
        }
        public RestApiClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            PostData = "";
        }
        public RestApiClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            PostData = "";
        }

        public RestApiClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            PostData = postData;
        }

        public string SendRequest()
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + Parameters);
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 OPR/26.0.1656.60";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization: Bearer " + AccessToken);

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

            using (var response = (HttpWebResponse)request.GetResponse())
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
    }
}
