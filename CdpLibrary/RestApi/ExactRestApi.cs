using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CdpLibrary.Auth;
using CdpLibrary.Helpers;

namespace CdpLibrary.RestApi
{
    public class ExactRestApi: IOAuth2 
    {
        public string _AuthUri
        {
            get
            { return "https://start.exactonline.co.uk/api/oauth2/auth"; }
        }
        public string _TokenUri
        {
            get
            { return "https://start.exactonline.co.uk/api/oauth2/token"; }
        }

        string _GETUri = "https://start.exactonline.co.uk/api/v1/{0}/documents/Documents";
        string _POSTUri = "https://start.exactonline.co.uk/api/v1/{0}/documents/Documents";
        string _UploadUri = "https://start.exactonline.co.uk/api/v1/{0}/documents/DocumentAttachments";

        public string Key { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string AuthCode { get; set; }
        public string GrantedToken { get; set; }

        public ExactRestApi(string division)
        {
            if (division == null) throw new Exception("Division must be provided.");

            _GETUri = string.Format(_GETUri, division);
            _POSTUri = string.Format(_POSTUri, division);
            _UploadUri = string.Format(_UploadUri, division);
        }

        public string GetAuthorizeUri()
        {
            return OAuth2.GetAuthorizeUri(
                OAuth2.OAuthResponseType.Code, _AuthUri, Key, RedirectUri).ToString();
        }

        public async Task<string> GetToken(string authCode)
        {
            AuthCode = authCode;

            if (GrantedToken == null)
            {
                var response = await OAuth2.GetToken(authCode, _TokenUri, Key, Secret, RedirectUri);
                GrantedToken = response.AccessToken; ;
            }
            return GrantedToken;
        }

        public ExactRestApiTypes.Documents GetDocuments()
        {
            try
            {
                RestApiHelper client = new RestApiHelper()
                {
                    EndPoint = _GETUri,
                    Method = RestApiHelper.HttpVerb.GET,
                    GrantedToken = GrantedToken,
                    Parameters = "?$select=ID,Subject,DocumentDate,Type,AccountName,Category,CategoryDescription"
                };
                var json = client.SendRequest();
                var obj = JsonHelper.ParseJson<ExactRestApiTypes.Root>(json);
                return obj.Documents;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to get all documents, error: " + ExceptionHelper.ExtractAll(ex));
            }
        }

        public ExactRestApiTypes.Document PostDocument(ExactRestApiTypes.Document doc)
        {
            try
            {
                string jsonPost = "{Subject : '" + doc.Subject + "',Type : 55,Category : '3b6d3833-b31b-423d-bc3c-39c62b8f2b12'}";

                RestApiHelper client = new RestApiHelper()
                {
                    EndPoint = _POSTUri,
                    Method = RestApiHelper.HttpVerb.POST,
                    GrantedToken = GrantedToken,
                    Parameters = "",
                    PostData = jsonPost
                };

                var json = client.SendRequest();
                var obj = JsonHelper.ParseJson<ExactRestApiTypes.Root_SingleDocument>(json);
                return obj.Document;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to post the document, error: " + ExceptionHelper.ExtractAll(ex));
            }
        }

        public string PostDocumentAttachment(string id, string filename, byte[] source)
        {
            try
            {
                string base64String = Convert.ToBase64String(source, 0, source.Length);
                string jsonPost = "{Attachment : " + base64String + ",Document : '" + id + "',FileName : '" + filename + "'}";

                RestApiHelper client = new RestApiHelper()
                {
                    EndPoint = _UploadUri,
                    Method = RestApiHelper.HttpVerb.POST,
                    GrantedToken = GrantedToken,
                    Parameters = "",
                    PostData = jsonPost
                };

                var json = client.SendRequest();
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to upload the attachment, error: " + ExceptionHelper.ExtractAll(ex));
            }

        }
    }
}
