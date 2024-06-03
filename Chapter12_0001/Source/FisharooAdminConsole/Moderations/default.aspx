<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Fisharoo.FisharooAdminConsole.Moderations._default" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:UpdatePanel runat="server">        
        <ContentTemplate>
            <table>
                <tr>
                    <td>Approve</td>
                    <td>Deny</td>
                    <td>Content</td>
                    <td>Reported User</td>
                    <td>Gag Till</td>
                    <td>Reason</td>
                </tr>
        <asp:Repeater ID="repModeration" runat="server" OnItemDataBound="repModeration_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td><asp:CheckBox ID="chkApprove" runat="server" /></td>
                    <td><asp:CheckBox ID="chkDeny" runat="server" /></td>
                    <td><asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder></td>
                    <td>
                        <asp:HyperLink runat="server" NavigateUrl='<%# _configuration.WebSiteURL + ((Moderation)Container.DataItem).AccountUsername %>' Text='<%#((Moderation)Container.DataItem).AccountUsername %>'></asp:HyperLink>
                        <asp:Literal ID="litSystemObjectID" Visible="false" Text='<%#((Moderation)Container.DataItem).SystemObjectID %>' runat="server"></asp:Literal>
                        <asp:Literal ID="litSystemObjectRecordID" Visible="false" Text='<%#((Moderation)Container.DataItem).SystemObjectRecordID %>' runat="server"></asp:Literal>
                        <asp:Literal ID="litAccountID" Visible="false" Text='<%#((Moderation)Container.DataItem).AccountID %>' runat="server"></asp:Literal>
                        <asp:Literal ID="litAccountUsername" Visible="false" Text='<%#((Moderation)Container.DataItem).AccountUsername %>' runat="server"></asp:Literal>
                    </td>
                    <td><asp:TextBox ID="txtGagDate" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtReason" runat="server"></asp:TextBox></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            </table>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>