using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using CdpLibrary.Auth;
using CdpLibrary.Helpers;

namespace CdpLibrary.RestApi.Providers
{
    internal class DropboxRestApi : IRestApi
    {
        public  string _AuthUri { get { return "https://www.dropbox.com/1/oauth2/authorize"; } }
        public string _TokenUri { get { return "https://api.dropbox.com/1/oauth2/token"; } }

        public string Key { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string AuthCode { get; set; }
        public string GrantedToken { get; set; }


        const string _GETUri = "https://api.dropboxapi.com/1/metadata/auto";
        const string _POSTUri = "https://api.dropbox.com/1/oauth2/token";
        const string _DownloadFileUri = "https://api-content.dropbox.com/1/files?root=sandbox&path=";

        public string GetAuthorizeUri()
        {
            return OAuth2.GetAuthorizeUri(
                OAuth2.OAuthResponseType.Code, this._AuthUri, this.Key, this.RedirectUri).ToString();
        }
        public async Task<string> GetToken(string authCode)
        {
            AuthCode = authCode;

            if (GrantedToken == null)
            {
                var response = await OAuth2.GetToken(authCode, this._TokenUri, this.Key, this.Secret, this.RedirectUri);
                this.GrantedToken = response.AccessToken;
            }
            return this.GrantedToken;
        }

        public IRestApiType GetFiles()
        {
            try
            {
                RestApiHelper client = new RestApiHelper()
                {
                    EndPoint = _GETUri,
                    Method = RestApiHelper.HttpVerb.GET,
                    GrantedToken = this.GrantedToken,
                    Parameters = "?list=true"
                };
                var json = client.SendRequest();
                var obj = JsonHelper.ParseJson<DropboxRestApiType.File>(json);

                // Convert DropboxRest File.Contents type to RestApi.Files type (IRestApiType)
                Files files = new Files();
                foreach (var f in obj.Contents)
                {
                    files.files.Add(
                        new RestApi.File()
                        {
                            id = f.Revision,
                            filename = f.Path.Replace("/", ""),
                            path = f.Path,
                            size = f.Size
                        }
                    );
                } 
                return files;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to get all documents, error: " + ExceptionHelper.ExtractAll(ex));
            }
        }
        
        public byte[] DownloadFile(string path)
        {
            try
            {
                RestApiHelper client = new RestApiHelper()
                {
                    EndPoint = _DownloadFileUri + path,
                    Method = RestApiHelper.HttpVerb.GET,
                    GrantedToken = this.GrantedToken
                };

                path = path.Replace("/", "\\");
                var filebytes = client.DownloadFile();
                
                return filebytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to download the file, error: " + ExceptionHelper.ExtractAll(ex));
            }
        }
    }
}
