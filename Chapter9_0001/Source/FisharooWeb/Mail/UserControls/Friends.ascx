<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Friends.ascx.cs" Inherits="Fisharoo.FisharooWeb.Mail.UserControls.Friends" %>
<div class="divContainerTitle" style="width:150px">Friends:</div>
<asp:Panel Width="150" ScrollBars="Vertical" Height="200" runat="server">
    <asp:Repeater OnItemDataBound="repFriends_ItemDataBound" ID="repFriends" runat="server">
        <ItemTemplate>
            <asp:HyperLink ID="linkFriend" NavigateUrl="javascript:void;" runat="server" Text='<%# ((Account)Container.DataItem).Username %>'></asp:HyperLink>
        </ItemTemplate>
        <SeparatorTemplate>
            <br />
        </SeparatorTemplate>
    </asp:Repeater>
</asp:Panel>