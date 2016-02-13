using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdpLibrary.Auth
{
    public class OAuth2Exception : Exception
    {
        public string ErrorDescription { get; private set; }

        public OAuth2Exception(string message, string errorDescription = null) : base(message)
        {
            this.ErrorDescription = errorDescription;
        }
    }
}
