using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using CdpLibrary.Helpers;

namespace CdpLibrary.Helpers
{
    public class RestApiHelper
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
        public string GrantedToken { get; set; }
        public string Parameters { get; set; }
        public string PostData { get; set; }

        public RestApiHelper()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            PostData = "";
        }

        public RestApiHelper(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            PostData = "";
        }

        public RestApiHelper(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            PostData = "";
        }

        public RestApiHelper(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            PostData = postData;
        }

        public string SendRequest()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + Parameters);
                request.Method = Method.ToString();
                request.Accept = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 OPR/26.0.1656.60";
                request.ContentType = "application/json; odata=verbose";
                request.Headers.Add("Authorization: Bearer " + GrantedToken);
                request.KeepAlive = true;

                if (!string.IsNullOrEmpty(this.PostData) && this.Method == HttpVerb.POST)
                {
                    //var encoding = new UTF8Encoding();
                    //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    //request.ContentLength = bytes.Length;

                    //using (var writeStream = request.GetRequestStream())
                    //{
                    //    writeStream.Write(bytes, 0, bytes.Length);
                    //}

                    /// Encode the parameters as form data:
                    //byte[] formData = UTF8Encoding.UTF8.GetBytes(this.PostData);
                    //request.ContentLength = formData.Length;

                    //// Send the request:
                    //using (Stream post = request.GetRequestStream())
                    //{
                    //    post.Write(formData, 0, formData.Length);
                    //}

                    var bytes = Encoding.GetEncoding("utf-8").GetBytes(this.PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    //if (response.StatusCode != HttpStatusCode.OK)
                    //{
                    //    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    //    throw new ApplicationException(message);
                    //}

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
            catch (WebException wex)
            {
                var RestError = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                throw new Exception((RestError == "") ? wex.Message : RestError);
            }
        }

        public byte[] DownloadFile()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + Parameters);
                request.Method = Method.ToString();
                request.Accept = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 OPR/26.0.1656.60";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization: Bearer " + GrantedToken);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    byte[] file;

                    using (Stream responseStream = response.GetResponseStream())
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        do
                        {
                            bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                            memoryStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);

                        file = memoryStream.ToArray();
                    }
                    return file;
                }
            }
            catch (WebException wex)
            {
                var RestError = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                throw new Exception((RestError == "") ? wex.Message : RestError);
            }
        }

        public string UploadFile()
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + Parameters);
            request.Method = Method.ToString();
            request.Accept = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 OPR/26.0.1656.60";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization: Bearer " + GrantedToken);

            byte[] buffer;
            using (var fileStream = new FileStream(
                @"D:\Readme.txt", FileMode.Open, FileAccess.Read))
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                fileStream.Read(buffer, 0, length);
            }

            request.ContentLength = buffer.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

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
