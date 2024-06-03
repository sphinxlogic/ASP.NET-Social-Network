<%@ Page MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="ViewGroup.aspx.cs" Inherits="Fisharoo.FisharooWeb.Groups.ViewGroup" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer">
    <div class="divContainerBox">
        <div class="divContainerRow">
            <asp:Label ForeColor="Red" ID="lblMessage" runat="server"></asp:Label>
            <asp:Panel ID="pnlPublic" runat="server">
                <div style="float:none;">
                    <div style="float:left;"><asp:Image ID="imgGroupLogo" runat="server" /></div>
                    <div style="text-align:right;">
                        <asp:Label ID="lblPrivateMessage" ForeColor="Red" runat="server" Text="This group is private!"></asp:Label> 
                        <asp:LinkButton ID="lbRequestMembership" OnClick="lbRequestMembership_Click" Text="Request Membership" runat="server"></asp:LinkButton>
                    </div>
                </div>
                Created: <asp:Label ID="lblCreateDate" runat="server"></asp:Label><br />
                Last Updated: <asp:Label ID="lblUpdateDate" runat="server"></asp:Label><br />
                <asp:Label ID="lblDescription" runat="server"></asp:Label><br /><br />
            </asp:Panel>
            <asp:Panel ID="pnlPrivate" runat="server">
                <asp:LinkButton ID="lbForum" OnClick="lbForum_Click" Text="View Forum" runat="server"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lbViewMembers" OnClick="lbViewMembers_Click" Text="Members" runat="server"></asp:LinkButton> 
                <asp:Label ID="lblBody" runat="server"></asp:Label>
            </asp:Panel>          
        </div>
    </div>
</div>
</asp:Content>