<%@ Page MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="ReadMessage.aspx.cs" Inherits="Fisharoo.FisharooWeb.Mail.ReadMessage" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerCell"><asp:Button style="text-align:left;" ID="btnReply" runat="server" OnClick="btnReply_Click" Text="Reply" /></div>
            <div class="divContainerCellHeader">From:</div>
            <div class="divContainerCell"><asp:HyperLink ID="linkFrom" runat="server"></asp:HyperLink></asp:Label></div>
            <div class="divContainerCellHeader">Subject:</div>
            <div class="divContainerCell"><asp:Label ID="lblSubject" runat="server"></asp:Label></div>
            <div class="divContainerCellHeader">Message:</div>
            <div class="divContainerCell"><asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br /></div>
        </div>
    </div>
</asp:Content>