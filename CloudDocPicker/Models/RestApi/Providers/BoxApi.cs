using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDocPicker.Models.RestApi
{
    public class BoxApi : RestApiBase
    {
        public override string AuthUri { get { return "https://app.box.com/api/oauth2/authorize"; } }
        public override string TokenUri { get { return "https://app.box.com/api/oauth2/token"; } }
        public override string GETUri { get { return ""; } }
        public override string POSTUri { get { return ""; } }

        public override string GetAllDoc()
        {
            return null;
        }

        public override string GetDoc(string id)
        {
            return "";
        }

        public override void PostDoc()
        {
        }
    }
}
