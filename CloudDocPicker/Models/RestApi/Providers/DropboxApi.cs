using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CloudDocPicker.Models.Helpers;

namespace CloudDocPicker.Models.RestApi
{
    public class DropboxApi : RestApiBase
    {
        public override string AuthUri { get { return "https://www.dropbox.com/1/oauth2/authorize"; } }
        public override string TokenUri { get { return "https://api.dropbox.com/1/oauth2/token"; } }
        public override string GETUri { get { return "https://api.dropboxapi.com/1/metadata/auto"; } }
        public override string POSTUri { get { return "https://api.dropbox.com/1/oauth2/token"; } }

        public override string GetAllDoc()
        {
            RestApiClient client = new RestApiClient()
            {
                EndPoint = this.GETUri,
                Method = RestApiClient.HttpVerb.GET,
                AccessToken = this.AccessToken,
                Parameters = "?list=true"
            };
            var json = client.SendRequest();
            return json;
        }

        public override string GetDoc(string id)
        {
            RestApiClient client = new RestApiClient()
            {
                EndPoint = "https://content.dropboxapi.com/1/files/auto/",
                Method = RestApiClient.HttpVerb.GET,
                AccessToken = this.AccessToken,
                Parameters = "?rev=" + id
            };
            var json = client.SendRequest();
            return json;
        }

        public override void PostDoc()
        {
        }
    }
}
