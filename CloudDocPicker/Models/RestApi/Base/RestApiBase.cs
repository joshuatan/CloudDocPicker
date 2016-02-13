using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CloudDocPicker.Models.Auth;

namespace CloudDocPicker.Models.RestApi
{
    public abstract class RestApiBase: IRestApiOAuth2, IRestApiDocOps 
    {
        public enum ApiProvider
        {
            [Description("Exact Online")]
            ExactOnline,
            [Description("Dropbox Online File Sharing")]
            Dropbox,
            [Description("Box Online File Sharing")]
            Box,
            [Description("Microsoft OneNote")]
            OneNote
        }

        public abstract string AuthUri { get; }
        public abstract string TokenUri { get; }
        public abstract string GETUri { get; }
        public abstract string POSTUri { get; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string RedirectUri { get; set; }
        public string AccessToken { get; private set; }
        public string TokenType { get; private set; }
        public string Uid { get; private set; }

        public virtual string GetAuthorizeUri()
        {
            return OAuth2.GetAuthorizeUri(
                OAuth2.OAuthResponseType.Code, this.AuthUri, this.AppKey, this.RedirectUri).ToString();
        }
        public virtual async Task<string> GetToken(string code)
        {
            var response = await OAuth2.GetToken(this.TokenUri, code, this.AppKey, this.AppSecret, this.RedirectUri);
            this.AccessToken = response.AccessToken;
            this.TokenType = response.TokenType;
            this.Uid = response.Uid;

            return this.AccessToken;
        }

        public abstract string GetAllDoc();
        public abstract string GetDoc(string id);
        public abstract void PostDoc();
    }
}
