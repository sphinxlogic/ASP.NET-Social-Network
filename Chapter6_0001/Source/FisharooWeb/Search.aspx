<%@ Page EnableEventValidation="false" MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Fisharoo.FisharooWeb.Search" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Register Src="~/UserControls/ProfileDisplay.ascx" TagPrefix="Fisharoo" TagName="ProfileDisplay" %>

<asp:Content ContentPlaceHolderID="SecondaryNav" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="LeftNavTop" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="LeftNavBottom" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle"><asp:Label ID="lblSearchTerm" runat="server"></asp:Label></div>
            <div class="divContainerRow" style="height:350px;">
               <div class="divContainerCell">
                    <asp:Panel ID="pnlFriends" Height="350" ScrollBars="Vertical" runat="server">
                        <asp:Repeater ID="repAccounts" runat="server" OnItemDataBound="repAccounts_ItemDataBound">
                            <ItemTemplate>
                                <Fisharoo:ProfileDisplay ShowDeleteButton="false" ID="pdProfileDisplay" runat="server"></Fisharoo:ProfileDisplay>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>