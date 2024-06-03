<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.ViewProfile" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer" style="min-height:210px;">
    <div class="divContainerRow">
        <div class="divContainerCell" style="float:left;width:210px;height:300px;">
            <asp:Image Width="200" Height="200" ID="imgAvatar" runat="server" ImageUrl="~/images/profileavatar/profileimage.aspx" /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        </div>
        <div class="divContainerCell">
            <asp:Label CssClass="ProfileName" ID="lblFirstName" runat="server"></asp:Label>
            <asp:Label CssClass="ProfileName" ID="lblLastName" runat="server"></asp:Label>
            <asp:Literal ID="litLevelOfExperience" runat="server"></asp:Literal>
            
<asp:Panel ID="pnlPrivacyAccountInfo" runat="server">
    <div class="divContainerTitle">Account Info</div>
    <div class="divInnerRowHeader">Email:</div>
    <div class="divInnerRowCell"><asp:Literal ID="litEmail" runat="server"></asp:Literal>&nbsp;</div>
    <div class="divInnerRowHeader">Zip:</div>
    <div class="divInnerRowCell"><asp:Literal ID="litZip" runat="server"></asp:Literal>&nbsp;</div>
    <div class="divInnerRowHeader">Birthday:</div>
    <div class="divInnerRowCell"><asp:Literal ID="litBirthDate" runat="server"></asp:Literal>&nbsp;</div>
    <div class="divInnerRowHeader">Updated:</div>
    <div class="divInnerRowCell"><asp:Literal ID="litLastUpdateDate" runat="server"></asp:Literal>&nbsp;</div><br />
</asp:Panel>
            
            <asp:Panel ID="pnlPrivacyIM" runat="server">
                <div class="divContainerTitle">Contact Information</div>
                <div class="divInnerRowHeader">AOL:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMAOL" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">MSN:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMMSN" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">GIM:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMGIM" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">YIM:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMYIM" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">ICQ:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMICQ" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">Skype:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litIMSkype" runat="server"></asp:Literal>&nbsp;</div>
            </asp:Panel>
            
            <asp:Panel ID="pnlPrivacyTankInfo" runat="server">
                <div class="divContainerTitle">Fish Facts</div>
                <div class="divInnerRowHeader">Year of tank:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litYearOfFirstTank" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">#Tanks owned:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litNumberOfTanksOwned" runat="server"></asp:Literal>&nbsp;</div>
                <div class="divInnerRowHeader">#Fish owned:</div>
                <div class="divInnerRowCell"><asp:Literal ID="litNumberOfFishOwned" runat="server"></asp:Literal>&nbsp;</div>
            </asp:Panel>
        </div>
    </div>
    <asp:PlaceHolder ID="phAttributes" runat="server"></asp:PlaceHolder>    
</div>
</asp:Content>