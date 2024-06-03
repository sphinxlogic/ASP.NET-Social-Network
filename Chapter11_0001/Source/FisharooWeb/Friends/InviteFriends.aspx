<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="InviteFriends.aspx.cs" Inherits="Fisharoo.FisharooWeb.Friends.InviteFriends" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Invite Your Friends</div>
            <asp:Panel ID="pnlInvite" runat="server">
                <div class="divContainerRow">
                    <div class="divContainerCellHeader">From:</div>
                    <div class="divContainerCell"><asp:Label ID="lblFrom" runat="server"></asp:Label></div>
                </div>
                <div class="divContainerRow">
                    <div class="divContainerCellHeader">To:<br /><div class="divContainerHelpText">(use commas to<BR />separate emails)</div></div>
                    <div class="divContainerCell"><asp:TextBox ID="txtTo" runat="server" TextMode="MultiLine" Columns="40" Rows="5"></asp:TextBox></div>
                </div>
                <div class="divContainerRow">
                    <div class="divContainerCellHeader">Message:</div>
                    <div class="divContainerCell"><asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Columns="40" Rows="10"></asp:TextBox></div>
                </div>
                <div class="divContainerFooter">
                    <asp:Button ID="btnInvite" runat="server" Text="Invite" OnClick="btnInvite_Click" />
                </div>
            </asp:Panel>
            <div class="divContainerRow">
                <div class="divContainerCell"><br /><asp:Label ID="lblMessage" runat="server"></asp:Label><br /><br /></div>
            </div>
        </div>
    </div>
</asp:Content>