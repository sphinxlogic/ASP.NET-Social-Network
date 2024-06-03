<%@ Page MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="OutlookCsvImporter.aspx.cs" Inherits="Fisharoo.FisharooWeb.Friends.OutlookCsvImporter" %>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="divContainer">
        <div class="divContainerBox">
            <asp:Panel ID="pnlUpload" runat="server">
                <div class="divContainerTitle">Import Contacts</div>
                <div class="divContainerRow">
                    <div class="divContainerCellHeader">Contacts File:</div>
                    <div class="divContainerCell"><asp:FileUpload ID="fuContacts" runat="server" /></div>
                </div>
                <div class="divContainerRow">
                    <div class="divContainerFooter"><asp:Button ID="btnUpload" Text="Upload & Preview Contacts" runat="server" OnClick="btnUpload_Click" /></div>
                </div>
                <br /><br />
                <div class="divContainerRow">
                    <div class="divContainerTitle">How do I export my contacts from Outlook?</div>
                    <div class="divContainerCell">
                        <ol>
                            <li>
                                Open Outlook
                            </li>
                            <li>
                                In the File menu choose Import and Export
                            </li>
                            <li>
                                Choose export to a file and click next
                            </li>
                            <li>
                                Choose comma seperated values and click next
                            </li>
                            <li>
                                Select your contacts and click next
                            </li>
                            <li>
                                Browse to the location you want to save your contacts file
                            </li>
                            <li>
                                Click finish
                            </li>
                        </ol>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel visible="false" ID="pnlEmails" runat="server">
                <div class="divContainerTitle">Select Contacts</div>
                <div class="divContainerFooter"><asp:Button ID="btnInviteContacts1" runat="server" OnClick="btnInviteContacts_Click" Text="Invite Selected Contacts" /></div>
                <div class="divContainerCell" style="text-align:left;">
                    <asp:CheckBoxList ID="cblEmails"  RepeatColumns="2" runat="server"></asp:CheckBoxList>
                </div>
                <div class="divContainerFooter"><asp:Button ID="btnInviteContacts2" runat="server" OnClick="btnInviteContacts_Click"  Text="Invite Selected Contacts" /></div>
            </asp:Panel>
            <asp:Panel ID="pnlResult" runat="server" Visible="false">
                <div class="divContainerTitle">Invitations Sent!</div>
                <div class="divContainerCell">
                    Invitations were sent to the following emails:<br />
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>