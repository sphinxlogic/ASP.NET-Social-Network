<%@ Page MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="Fisharoo.FisharooAdminConsole.Account.Accounts" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
    <asp:GridView ID="gvAccounts" runat="server"></asp:GridView>
</asp:Content>