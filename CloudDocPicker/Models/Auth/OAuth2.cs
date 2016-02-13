using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using CloudDocPicker.Models.Helpers;

namespace CloudDocPicker.Models.Auth
{
    public static class OAuth2
    {
        public enum OAuthResponseType
        {
            Code = 0,
            Token = 1
        }
        
        public static Uri GetAuthorizeUri(
            OAuthResponseType oauthResponseType, 
            string authUri, 
            string clientId, 
            string redirectUri = null)
        {
            bool forceReapprove = false;
            bool disableSignup = false;

            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException("clientId");
            if (redirectUri == null && oauthResponseType != OAuthResponseType.Code) throw new ArgumentNullException("redirectUri");

            var queryBuilder = new StringBuilder();

            queryBuilder.Append("response_type=");
            switch (oauthResponseType)
            {
                case OAuthResponseType.Token:
                    queryBuilder.Append("token");
                    break;
                case OAuthResponseType.Code:
                    queryBuilder.Append("code");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("oauthResponseType");
            }

            queryBuilder.Append("&client_id=").Append(Uri.EscapeDataString(clientId));

            if (redirectUri != null)
            {
                queryBuilder.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri));
            }

            if (forceReapprove)
            {
                queryBuilder.Append("&force_reapprove=true");
            }

            if (disableSignup)
            {
                queryBuilder.Append("&disable_signup=true");
            }

            var uriBuilder = new UriBuilder(authUri)
            {
                Query = queryBuilder.ToString()
            };

            return uriBuilder.Uri;
        }

        public async static Task<OAuth2Response> GetToken(
            string tokenUri,
            string code,
            string appKey,
            string appSecret,
            string redirectUri = null)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            else if (string.IsNullOrEmpty(appKey))
            {
                throw new ArgumentNullException("appKey");
            }
            else if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentNullException("appSecret");
            }

            var httpClient = new HttpClient();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "client_id", appKey },
                    { "client_secret", appSecret }
                };

                if (!string.IsNullOrEmpty(redirectUri))
                {
                    parameters["redirect_uri"] = redirectUri;
                }

                var content = new FormUrlEncodedContent(parameters);
                var response = await httpClient.PostAsync(tokenUri, content);

                var raw = await response.Content.ReadAsStringAsync();
                //var raw = response.Content.ReadAsStringAsync().Result;
                var json = JsonHelper.JObjectParse(raw);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new OAuth2Exception(json["error"].ToString(), json.Value<string>("error_description"));
                }

                return JsonHelper.ParseJson<OAuth2Response>(raw);
            }
            finally
            {
                httpClient.Dispose();
            }
        }
    }
}
