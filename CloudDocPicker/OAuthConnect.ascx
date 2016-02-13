<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OAuthConnect.ascx.cs" Inherits="CloudDocPicker.OAuthConnect" %>

<div class="rTable">
    <div class="rTableRow" id="trProvider" runat="server">
        <div class="rTableHead">Provider</div>
        <div class="rTableCell"><asp:DropDownList ID="ddlProvider" runat="server"></asp:DropDownList></div>
    </div>
    <div class="rTableRow">
        <div class="rTableHead">Application Key</div>
        <div class="rTableCell"><asp:TextBox ID="txtAppKey" Width="300px" runat="server"></asp:TextBox></div>
    </div>
    <div class="rTableRow"></div>
    <div class="rTableRow">
        <div class="rTableHead">Application Secret</div> 
        <div class="rTableCell"><asp:TextBox ID="txtAppSecret"  Width="300px" runat="server"></asp:TextBox></div> 
    </div>
    <div class="rTableRow">
        <div class="rTableHead">&nbsp;</div> 
        <div class="rTableCell">
            <asp:Button ID="btnConnectExact" Text="Connect" runat="server" OnClick="btnConnectExact_Click" />
            <asp:Button ID="btnConnectProvider" Text="Connect" runat="server" OnClick="btnConnectProvider_Click" />
        </div> 
    </div>
</div>
                
    
