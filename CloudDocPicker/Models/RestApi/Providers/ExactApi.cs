using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDocPicker.Models.Auth;

namespace CloudDocPicker.Models.RestApi
{
    public class ExactApi : RestApiBase
    {
        public override string AuthUri { get { return "https://start.exactonline.co.uk/api/oauth2/auth"; } }
        public override string TokenUri { get { return "https://start.exactonline.co.uk/api/oauth2/token"; } }
        public override string GETUri { get { return "https://start.exactonline.co.uk/api/v1/31622/documents/Documents"; } }
        public override string POSTUri { get { return "https://start.exactonline.co.uk/api/v1/31622/documents/Documents"; } }

        public override string GetAllDoc()
        {
            RestApiClient client = new RestApiClient()
            {
                EndPoint = this.GETUri,
                Method = RestApiClient.HttpVerb.GET,
                AccessToken = this.AccessToken,
                Parameters = "?$select=ID,Subject,DocumentDate,Type,AccountName,CategoryDescription"
            };
            var json = client.SendRequest();
            return json;
        }
        public override string GetDoc(string id)
        {
            return "";
        }
        public override void PostDoc()
        {
            RestApiClient client = new RestApiClient()
            {
                EndPoint = this.POSTUri,
                Method = RestApiClient.HttpVerb.POST,
                AccessToken = this.AccessToken,
                PostData = "{CategoryDescription:'test',DocumentDate:2015-01-01,Subject:'Test Document',Type:5}"
            };
        }
    }
}
