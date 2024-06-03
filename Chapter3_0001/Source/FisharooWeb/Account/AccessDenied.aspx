<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Fisharoo.FisharooWeb.Account.AccessDenied" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerRow">
            You do not have the appropriate permissions to access that page or area of our site!  If you feel that you have received this message in error, please try logging off our system and then logging back in.  If that does not address your issue, please feel free to contact us.<br /><br />
            Sorry for any inconvenience.<br /><br />
            ~Fisharoo
        </div>
    </div>
</asp:Content>