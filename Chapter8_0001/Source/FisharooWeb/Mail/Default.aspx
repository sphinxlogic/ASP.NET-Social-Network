<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Mail.Default" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Register Src="~/Mail/UserControls/Folders.ascx" TagPrefix="Fisharoo" TagName="Folders" %>
<asp:Content ContentPlaceHolderID="LeftNavTop" runat="server">
    <Fisharoo:Folders id="Folders1" runat="server"></Fisharoo:Folders>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnDelete" Text="Delete" runat="server" OnClick="btnDelete_Click" />
                    </td>
                    <td colspan="2">
                        <asp:HyperLink ID="linkPrevious" runat="server" Text="Previous" />
                        <asp:PlaceHolder ID="phPages" runat="server"></asp:PlaceHolder>
                        <asp:HyperLink ID="linkNext" runat="server" Text="Next" />
                    </td>
                </tr>
                <asp:Repeater ID="repMessages" runat="server" OnItemDataBound="repMessages_ItemDataBound">
                    <ItemTemplate>
                            <tr>
                                <td width="10"><asp:CheckBox ID="chkMessage" runat="server" /></td>
                                <td width="100" align="left"><asp:HyperLink ID="linkProfile" runat="server" Text='<%# ((MessageWithRecipient)Container.DataItem).Sender.Username %>'></asp:HyperLink></td>
                                <td align="left"><asp:HyperLink ID="linkMessage" runat="server" Text='<%# ((MessageWithRecipient)Container.DataItem).Message.Subject %>'></asp:HyperLink></td>
                            </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>