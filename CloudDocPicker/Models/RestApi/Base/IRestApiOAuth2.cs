using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDocPicker.Models.RestApi
{
    public interface IRestApiOAuth2
    {
        string AppKey { get; set; }
        string AppSecret { get; set; }
        string RedirectUri { get; set; }
        string AccessToken { get; }
        string TokenType { get; }
        string Uid { get; }

        string GetAuthorizeUri();
        Task<string> GetToken(string code);
    }
}
