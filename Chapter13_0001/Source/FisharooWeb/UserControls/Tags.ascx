<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tags.ascx.cs" Inherits="Fisharoo.FisharooWeb.UserControls.Tags" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:Panel runat="server" ID="pnlTag" Visible="false">
        <asp:TextBox ID="txtTag" runat="server"></asp:TextBox>
        <asp:Button ID="btnTag" runat="server" Text="Tag It!" OnClick="btnTag_Click" />
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlTagCloud" Visible="false">
        <asp:PlaceHolder ID="phTagCloud" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>


