<%@ Page EnableEventValidation="false" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Friends.Default" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Register Src="~/UserControls/ProfileDisplay.ascx" TagPrefix="Fisharoo" TagName="ProfileDisplay" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Friends</div>
            <asp:Repeater ID="repFriends" runat="server" OnItemDataBound="repFriends_ItemDataBound">
                <ItemTemplate>
                    <div class="divContainerRow" style="height:110px;">
                        <div class="divContainerCell">
                            <Fisharoo:ProfileDisplay ShowFriendRequestButton="false" ID="pdProfileDisplay" runat="server" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>