<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManagePrivacy.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.ManagePrivacy" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerTitle">Set the visibility of each section below:</div>
        <div class="divContainerRow">
            <div class="divContainerCellHeader">Private:</div>
            <div class="divContainerCell">Only you can see it</div>
        </div>
        <div class="divContainerRow">
            <div class="divContainerCellHeader">Friends Only:</div>
            <div class="divContainerCell">Only you and your friends can see it</div>
        </div>
        <div class="divContainerRow">
            <div class="divContainerCellHeader">Public:</div>
            <div class="divContainerCell">Everyone can see it</div>
        </div>
        <div class="divContainerRow">&nbsp;</div>
        <div class="divContainerRow">
            <asp:PlaceHolder ID="phPrivacyFlagTypes" runat="server"></asp:PlaceHolder>
        </div>
        <div class="divContainerFooter">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="Save Privacy Settings" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>