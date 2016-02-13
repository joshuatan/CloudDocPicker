using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CdpLibrary.Helpers
{
    public static class UiHelper
    {
        public static Control GetControlThatCausedPostBack(Page page)
        {
            //initialize a control and set it to null
            Control ctrl = null;

            //get the event target name and find the control
            string ctrlName = page.Request.Params.Get("__EVENTTARGET");
            if (!String.IsNullOrEmpty(ctrlName))
                ctrl = page.FindControl(ctrlName);

            //return the control to the calling method
            return ctrl;
        }

        public static string ErrorDecor(string error)
        {
            return "<div style='text-align:center'><span style='display: inline-block;font-size:18px;background-color:#F5A9A9;padding:10px'>“" + error + "”</span></div>";
        }

        public static string MessageDecor(string message)
        {
            return "<div style='text-align:center'><span style='display: inline-block;font-size:18px;background-color:#F2F5A9;padding:10px'>“" + message + "”</span></div>";
        }
    }
}
