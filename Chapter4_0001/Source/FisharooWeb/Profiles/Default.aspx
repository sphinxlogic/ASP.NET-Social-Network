<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.Default" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>
<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer">
<div class="divContainerTitle">The Filter</div>
    <asp:Repeater ID="repFilter" runat="server">
        <ItemTemplate>
            <asp:Label ID="lblMessage" runat="server" Text='<%# ((Alert)Container.DataItem).Message  %>'></asp:Label>
        </ItemTemplate>
        <SeparatorTemplate>
            <div class="AlertSeparator"></div>
        </SeparatorTemplate>
    </asp:Repeater>
</div>
</asp:Content>