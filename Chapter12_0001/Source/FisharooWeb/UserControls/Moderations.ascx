<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Moderations.ascx.cs" Inherits="Fisharoo.FisharooWeb.UserControls.Moderations" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlFlagThis" runat="server" style="float:left; padding-left:5px;padding-right:5px;">
            <asp:ImageButton ToolTip="Flag this content!" ID="ibFlagThis" runat="server" ImageUrl="~/images/icon_flag.gif" OnClick="ibFlagThis_Click" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>