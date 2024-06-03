<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogInLinks.ascx.cs" Inherits="Fisharoo.FisharooWeb.UserControls.LogInLinks" %>

<asp:Panel ID="pnlLoggedIn" runat="server">
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="lbHome" Text="Home" runat="server" OnClick="lbHome_Click"></asp:LinkButton> - 
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="lbEditAccount" Text="Edit Account" runat="server" OnClick="lbEditAccount_Click"></asp:LinkButton> - 
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="lbLogOut" Text="Log Out" runat="server" OnClick="lbLogOut_Click"></asp:LinkButton> -
    <asp:Label CssClass="TipTopNavigationLinks" ID="lblUsername" runat="server"></asp:Label>
</asp:Panel>

<asp:Panel ID="pnlNotLoggedIn" runat="server">
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="LinkButton1" Text="Home" runat="server" OnClick="lbHome_Click"></asp:LinkButton> - 
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="lbLogin" Text="Log In" runat="server" OnClick="lbLogin_Click"></asp:LinkButton> - 
    <asp:LinkButton CausesValidation="false" CssClass="TipTopNavigationLinks" ID="lbRegister" Text="Register" runat="server" OnClick="lbRegister_Click"></asp:LinkButton>
</asp:Panel>