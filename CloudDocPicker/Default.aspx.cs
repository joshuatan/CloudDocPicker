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
    public partial class _Default : Page
    {
        protected OAuthConnect ocProvider;
        protected OAuthConnect ocExact;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region "Load User Control"

                ocProvider = LoadControl("~/OAuthConnect.ascx") as OAuthConnect;
                ocProvider.ID = "provider";
                ocProvider.AppKey = "ex5z6bilhbscg5o";
                ocProvider.AppSecret = "ydwidva3d94otoa";
                ocProvider.IsForConnectExact = false;
                phConnectPvr.Controls.Add(ocProvider);
                phFilePvr.Visible = false;

                ocExact = LoadControl("~/OAuthConnect.ascx") as OAuthConnect;
                ocExact.ID = "exact";
                ocExact.AppKey = Global.ExactRestApi.Key;
                ocExact.AppSecret = Global.ExactRestApi.Secret;
                ocExact.IsForConnectExact = true;
                phConnectExt.Controls.Add(ocExact);
                phFileExt.Visible = false;

                #endregion

                this.ocProvider.ConnectProviderClick += new EventHandler(ocProvider_ConnectClick);
                this.ocExact.ConnectExactClick += new EventHandler(ocExact_ConnectClick);

                GetTokenAndSetPanel();
                SetUiPanel();
            }
            catch (Exception ex)
            {
                Response.Write(ExceptionHelper.ExtractAll(ex));
            }
        }

        protected void SetUiPanel()
        {
            if (Global.ProviderRestApi != null) if (Global.ProviderRestApi.GrantedToken != null) SetCloudProviderPanel();
            if (Global.ExactRestApi.GrantedToken != null) SetExactOnlinePanel();
        }
        
        protected void ocProvider_ConnectClick(object sender, EventArgs e)
        {
            try
            {
                Global.ProviderRestApi = Factory.Create(ocProvider.SelectedProvider);
                Global.ProviderRestApi.Key = ocProvider.AppKey;
                Global.ProviderRestApi.Secret = ocProvider.AppSecret;
                Global.ProviderRestApi.RedirectUri = Global.AppUri + "?authfrom=provider";

                var uri = Global.ProviderRestApi.GetAuthorizeUri();
                Response.Redirect(uri);
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        protected void ocExact_ConnectClick(object sender, EventArgs e)
        {
            try
            {
               var uri = Global.ExactRestApi.GetAuthorizeUri();
               Response.Redirect(uri);
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }
        
        protected void btnRefreshProvider_Click(object sender, EventArgs e)
        {
            try
            {
                IRestApiType files = Global.ProviderRestApi.GetFiles();
                lstFile.Items.Clear();
                foreach (var f in files.files)
                {
                    lstFile.Items.Add(new ListItem(f.filename, f.filename));
                }

                btnPick.Enabled = (lstFile.Items.Count > 0);
            }
            catch (Exception ex )
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        protected void btnPick_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstFile.SelectedValue != "")
                {
                    if (Global.ExactRestApi.GrantedToken != null)
                    {
                        var fileBytes = Global.ProviderRestApi.DownloadFile(lstFile.SelectedValue);
                        var obj = Global.ExactRestApi.PostDocument(
                            new ExactRestApiTypes.Document() { Subject = lstFile.SelectedValue }
                        );
                        Global.ExactRestApi.PostDocumentAttachment(obj.ID, obj.Subject, fileBytes);

                        Response.Write(UiHelper.MessageDecor("Selected file is been picked to Exact Online."));
                    }
                    else
                    {
                        Response.Write(UiHelper.MessageDecor("Please connect to Exact Online."));
                    }
                }
                else
                {
                    Response.Write(UiHelper.MessageDecor("Please select any file from Dropbox."));
                }
            }
            catch (KeyNotFoundException)
            {
                Response.Write(UiHelper.MessageDecor("Please connect to Exact Online."));
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        protected void btnRefreshExact_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = Global.ExactRestApi.GetDocuments();
                gvDocument.DataSource = obj.Results;
                gvDocument.DataBind();
                gvDocument.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        protected void GetTokenAndSetPanel()
        {
            try
            {
                string qAuthFrom = Request.QueryString["authfrom"];
                string qAuthCode = Request.QueryString["code"];

                if (qAuthFrom != null && qAuthCode != null)
                {
                    #region "Get Access Token"

                    switch (qAuthFrom)
                    {
                        case "exact":
                            Global.ExactRestApi.AuthCode = qAuthCode;
                            Page.RegisterAsyncTask(new PageAsyncTask(GetExactToken));
                            SetExactOnlinePanel();
                            break;
                        case "provider":
                            Global.ProviderRestApi.AuthCode = qAuthCode;
                            Page.RegisterAsyncTask(new PageAsyncTask(GetProviderToken));
                            SetCloudProviderPanel();
                            break;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Response.Write(UiHelper.ErrorDecor(ExceptionHelper.ExtractAll(ex)));
            }
        }

        private async Task GetProviderToken()
        {
            try
            {
                await Global.ProviderRestApi.GetToken(Global.ProviderRestApi.AuthCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetExactToken()
        {
            try
            {
                await Global.ExactRestApi.GetToken(Global.ExactRestApi.AuthCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetCloudProviderPanel()
        {
            lblCloudProviderStatus.Text = "Connected";
            lblCloudProviderStatus.ForeColor = System.Drawing.Color.Green;

            phConnectPvr.Visible = false;
            phFilePvr.Visible = true;
        }

        public void SetExactOnlinePanel()
        {
            lblExactOnlineStatus.Text = "Connected";
            lblExactOnlineStatus.ForeColor = System.Drawing.Color.Green;

            phConnectExt.Visible = false;
            phFileExt.Visible = true;
        }
    }
}