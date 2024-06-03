<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManageGroup.aspx.cs" Inherits="Fisharoo.FisharooWeb.Groups.ManageGroup" %>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                <div class="divContainerCellHeader">Name:
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></div>
                <div class="divContainerCell"><asp:TextBox ID="txtName" runat="server"></asp:TextBox></div>
                <div class="divContainerCellHeader">Public:</div>
                <div class="divContainerCell"><asp:CheckBox id="chkIsPublic" runat="server" /></div>
                <div class="divContainerCellHeader">PageName:<asp:RequiredFieldValidator runat="server" ControlToValidate="txtPageName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></div> 
                <div class="divContainerCell"><asp:TextBox ID="txtPageName" runat="server"></asp:TextBox></div>
                <div class="divContainerCellHeader">Logo:</div>
                <div class="divContainerCell"><asp:FileUpload ID="fuLogo" runat="server" /></div>
                <div class="divContainerCell"><asp:Image ID="imgLogo" runat="server" /></div>
                
                <div class="divContainerCellHeader">Description:<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></div><div class="divContainerCell">&nbsp;</div>
                <div class="divContainerCell"><asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server"></asp:TextBox></div>
                <div class="divContainerCell"><div style="font-size:10px;color:#FF0000;">This text is public and will be seen by all!</div></div>
                
                <div class="divContainerCellHeader">Page Body:<asp:RequiredFieldValidator runat="server" ControlToValidate="txtBody" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></div><div class="divContainerCell">&nbsp;</div>
                <div class="divContainerCell"><asp:TextBox TextMode="MultiLine" ID="txtBody" runat="server"></asp:TextBox></div>
                <div class="divContainerCell"><div style="font-size:10px;color:#FF0000;">This text is private and will only be seen by members if this is a private group!</div></div>
                
                <div class="divContainerCellHeader">Group Types:</div>
                <div class="divContainerCell"><asp:ListBox ID="lbGroupTypes" runat="server" SelectionMode="Multiple"></asp:ListBox></div>
                
                <asp:Literal Visible="false" ID="litGroupID" runat="server" Text="0"></asp:Literal>
                <asp:Literal Visible="false" ID="litAccountID" runat="server"></asp:Literal>
                <asp:Literal Visible="false" ID="litCreateDate" runat="server"></asp:Literal>
                <asp:Literal Visible="false" ID="litUpdateDate" runat="server"></asp:Literal>
                <asp:Literal Visible="false" ID="litMemberCount" runat="server"></asp:Literal>
                <asp:Literal Visible="false" ID="litTimestamp" runat="server"></asp:Literal>
                <asp:Literal Visible="false" ID="litFileID" Text="0" runat="server"></asp:Literal>
            </div>  
            <div class="divContainerFooter">
                <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" runat="server" />
            </div>
        </div>
    </div>
    
    
    <script type="text/javascript">
        xinha_editors[xinha_editors.length] = 'ctl00_Content_txtDescription';
        xinha_editors[xinha_editors.length] = 'ctl00_Content_txtBody';
    </script> 
</asp:Content>