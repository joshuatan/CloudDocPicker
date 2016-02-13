using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CdpLibrary.RestApi;

namespace CdpLibrary.RestApi.Providers
{
    internal class OneNoteRestApi : IRestApi
    {
        public string _AuthUri { get { return "https://www.dropbox.com/1/oauth2/authorize"; } }
        public string _TokenUri { get { return "https://api.dropbox.com/1/oauth2/token"; } }

        public string Key { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string AuthCode { get; set; }
        public string GrantedToken { get; set; }

        public string GetAuthorizeUri() { throw new NotImplementedException(); }

        public Task<string> GetToken(string code) { throw new NotImplementedException(); }

        public IRestApiType GetFiles() { throw new NotImplementedException(); }

        public byte[] DownloadFile(string path) { throw new NotImplementedException(); }
    }
}
