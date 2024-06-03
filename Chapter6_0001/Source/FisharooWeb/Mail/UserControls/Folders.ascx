<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Folders.ascx.cs" Inherits="Fisharoo.FisharooWeb.Mail.UserControls.Folders" %>
<div class="divContainerTitle" style="width:150px;">Folders</div>
<asp:Repeater ID="repFolders" runat="server" OnItemDataBound="repFolders_ItemDataBound">
    <ItemTemplate><div class="Folders"><asp:HyperLink NavigateUrl="javascript:void;" ID="linkFolder" runat="server"></asp:HyperLink></div></ItemTemplate>
</asp:Repeater>