<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="Fisharoo.FisharooWeb.Account.EditAccount" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:Panel ID="pnlEditAccount" runat="server">
        <div class="divContainer">
            <div class="divContainerRow">
                <div class="divContainerTitle">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    <asp:ValidationSummary ID="Summary" runat="server" DisplayMode="List" ForeColor="Red" />
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Username:
                </div>
                <div class="divContainerCell">
                    <asp:Label ID="lblUsername" runat="server" ForeColor="Gray"></asp:Label>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Old Password:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="valOldPasswordRequired" 
                        runat="server" 
                        ControlToValidate="txtOldPassword" 
                        ForeColor="Red" 
                        ErrorMessage="In order to make changes to your account you need to provide your current password.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    New Password:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        ID="valNewPassword" 
                        runat="server" 
                        ForeColor="Red"
                        ControlToValidate="txtNewPassword"
                        ValidationExpression="(?=^.{5,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"
                        Display="Dynamic"
                        ErrorMessage="Your password must be at least 8 characters long and contain at
                         least one upper case letter, one lower case letter, one number, and one special character">*</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Verify Password:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtVerifyPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator 
                        ID="valComparePasswords" 
                        runat="server" 
                        ForeColor="Red"
                        ControlToValidate="txtNewPassword" 
                        ControlToCompare="txtVerifyPassword" 
                        ErrorMessage="The passwords you entered do no match!" 
                        Display="Dynamic">*</asp:CompareValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    First Name:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Last Name:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Email:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="valRequiredEmail" 
                        runat="server" 
                        ForeColor="Red" 
                        ControlToValidate="txtEmail" 
                        ErrorMessage="Please provide your email address!">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator 
                        ID="valEmailInCorrectFormat" 
                        runat="server" 
                        ForeColor="Red"
                        ErrorMessage="This does not appear to be a valid email address!" 
                        ControlToValidate="txtEmail"     
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Birthdate:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>
                    <asp:CompareValidator 
                        ID="valDate" 
                        runat="server" 
                        ForeColor="Red" 
                        ControlToValidate="txtBirthDate" 
                        Type="Date" 
                        Operator="DataTypeCheck"
                        ErrorMessage="Please enter a valid date!">*</asp:CompareValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Zip:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        ID="valZipcode" 
                        ControlToValidate="txtZipCode"
                        runat="server" 
                        ForeColor="Red" 
                        ErrorMessage="This must be a valid US zip code!" 
                        ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$">*</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    &nbsp;
                </div>
                <div class="divContainerCell">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSaved" runat="server">
        
    </asp:Panel>
</asp:Content>