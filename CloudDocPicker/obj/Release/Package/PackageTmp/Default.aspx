<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CloudDocPicker._Default" Async="true" trace="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="rTable">
        <div class="rTableHeading">
            Cloud Provider&nbsp;
            <asp:Label ID="lblCloudProviderStatus" Text="Not Connected" ForeColor="Gray" runat="server"></asp:Label>
        </div>
        <div class="rTableRow">
            <asp:Panel ID="pnlCloudProvider" runat="server" Width="800px" Height="250px">
                <asp:PlaceHolder runat="server" ID="phConnectPvr" ></asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phFilePvr" >
                    <div class="rTable">
                        <div class="rTableRow">
                            <div class="rTableCell" style="height:170px;width:530px;background-color:#EFF8FB;border:1px dashed white">
                                <asp:ListBox ID="lstFile" Width="500px" Height="160px" runat="server"></asp:ListBox>
                            </div>
                        </div>
                        <div class="rTableRow">
                            <div class="rTableCell" style="width:530px;text-align:right;background-color:#CEECF5;border:1px dashed white">
                                <asp:Button ID="btnRefreshPvr" Text="Refresh" runat="server" OnClick="btnRefreshProvider_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPick" Text="Pick It" runat="server" Enabled="false" OnClick="btnPick_Click" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </asp:Panel>
        </div>
    </div>
     <div class="rTable">
        <div class="rTableHeading">
            Exact Online&nbsp;
            <asp:Label ID="lblExactOnlineStatus" Text="Not Connected" ForeColor="Gray" runat="server"></asp:Label>
        </div>
        <div class="rTableRow">
            <asp:Panel ID="pnlExactOnline" runat="server" Width="1100px" Height="500px">
                <asp:PlaceHolder runat="server" ID="phConnectExt" ></asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phFileExt" >
                    <div class="rTable">
                        <div class="rTableRow">
                            <div class="rTableCell" style="height:200px;width:1080px;background-color:#EFF8FB;border:1px dashed white">
                                <asp:GridView ID="gvDocument" PageSize="100" Width="100%" runat="server"></asp:GridView>
                            </div>
                        </div>
                        <div class="rTableRow">
                            <div class="rTableCell" style="width:1080px;text-align:right;background-color:#CEECF5;border:1px dashed white">
                                <asp:Button ID="btnRefreshExt" Text="Refresh" runat="server" OnClick="btnRefreshExact_Click" />
                            </div>
                        </div>
                    </div>
                    
                </asp:PlaceHolder>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
