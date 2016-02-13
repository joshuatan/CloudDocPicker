using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using CdpLibrary.RestApi;

namespace CloudDocPicker
{
    public class Global : HttpApplication
    {
        public static string AppUri = @"https://itech-demo.no-ip.biz:8812";     //@"http://localhost:8467";
        readonly string ExactDivision = "31622";

        public static ExactRestApi ExactRestApi;
        public static IRestApi ProviderRestApi;
        


        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ExactRestApi = new ExactRestApi(ExactDivision)
            {
                Key = "2ea641c9-faff-4f80-a2ea-c286653a567f",
                Secret = "DtZvBzTATw43",
                RedirectUri = AppUri + "?authfrom=exact"
            };
        }
    }
}