<%@ Page ValidateRequest="false" MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="Fisharoo.FisharooWeb.Mail.NewMessage" %>
<%@ Register Src="~/Mail/UserControls/Friends.ascx" TagPrefix="Fisharoo" TagName="Friends" %>
<asp:Content ContentPlaceHolderID="LeftNavTop" runat="server">
    <Fisharoo:Friends ID="friends1" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:Panel ID="pnlSendMessage" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <div class="divContainerCellHeader">To:</div>
                <div class="divContainerCell"><asp:TextBox Width="400" ID="txtTo" runat="server"></asp:TextBox></div>               
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Subject:</div>
                <div class="divContainerCell"><asp:TextBox Width="400" ID="txtSubject" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <asp:TextBox TextMode="MultiLine" ID="txtMessage" runat="server"></asp:TextBox>
            </div>
            <div class="divContainerFooter">
                <asp:Button ID="btnSend" Text="Send" runat="server" OnClick="btnSend_Click" />
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        xinha_editors[xinha_editors.length] = 'ctl00_Content_txtMessage';
    </script> 
    
    </asp:Panel>
    <asp:Panel Visible="false" runat="server" ID="pnlSent">
        <div class="divContainer">
            <div class="divContainerBox">
                <div class="divContainerRow">
                    <div class="divContainerCell">
                        Your message was sent!
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>