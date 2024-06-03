<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="Fisharoo.FisharooWeb.UserControls.Comments" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlComment">
            <asp:TextBox ID="txtComment" runat="server"></asp:TextBox><asp:Button Text="Add Comment" ID="btnAddComment" runat="server" OnClick="btnAddComment_Click" />
            <asp:PlaceHolder ID="phComments" runat="server"></asp:PlaceHolder>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
    