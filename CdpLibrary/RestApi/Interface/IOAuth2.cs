using System;
using System.Threading.Tasks;

namespace CdpLibrary.RestApi
{
    public interface IOAuth2
    {
        string _AuthUri { get; }
        string _TokenUri { get; }
        string Key { get; set; }
        string Secret { get; set; }
        string RedirectUri { get; set; }
        string AuthCode { get; set; }
        string GrantedToken { get; }

        string GetAuthorizeUri();
        Task<string> GetToken(string code);
    }
}
