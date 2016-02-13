using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using CdpLibrary.RestApi;
using CdpLibrary.Helpers;

namespace CloudDocPicker
{
    public partial class OAuthConnect : System.Web.UI.UserControl
    {
        public event EventHandler ConnectExactClick;
        public event EventHandler ConnectProviderClick;

        public bool IsForConnectExact { get; set; }
        public string AppKey
        {
            get { return txtAppKey.Text; }
            set { txtAppKey.Text = value; }
        }
        public string AppSecret
        {
            get { return txtAppSecret.Text; }
            set { txtAppSecret.Text = value; }
        }
        public Provider SelectedProvider {
            get {
                Provider provider = Provider.Dropbox;
                if (ddlProvider.Items.Count > 0) provider = 
                        (Provider)int.Parse(ddlProvider.SelectedValue);
                return provider;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindProvider(ref ddlProvider);

                btnConnectExact.Visible = (IsForConnectExact == true);
                btnConnectProvider.Visible = !(IsForConnectExact == true);
                trProvider.Visible = !(IsForConnectExact == true);
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        private void BindProvider(ref DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
                foreach (Provider provider in EnumHelper.EnumToList<Provider>())
                {
                    var item = new ListItem()
                    {
                        Text = EnumHelper.GetEnumDescription(provider),
                        Value = ((int)provider).ToString()
                    };
                    ddl.Items.Add(item);
                }
                ddl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnConnectExact_Click(object sender, EventArgs e)
        {
            ConnectExactClick(sender, e);
        }

        protected void btnConnectProvider_Click(object sender, EventArgs e)
        {
            ConnectProviderClick(sender, e);
        }
    }
}