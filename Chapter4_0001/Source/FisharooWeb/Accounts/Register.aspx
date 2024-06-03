<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Fisharoo.FisharooWeb.Accounts.Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:Panel ID="pnlCreateAccount" runat="server">
        <div class="divContainer">
            <div class="divContainerRow">
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>  
                <asp:Wizard OnNextButtonClick="wizRegister_NextButtonClick" OnActiveStepChanged="wizRegister_ActiveStepChanged" ID="wizRegister" OnFinishButtonClick="wizRegister_FinishButtonClicked" SideBarStyle-CssClass="WizardSideBar" CssClass="Wizard"  runat="server">
                    <HeaderTemplate>
                        <asp:ValidationSummary ID="valSummary" runat="server" HeaderText="All fields are required!" DisplayMode="BulletList" ForeColor="Red" />
                    </HeaderTemplate>
                    <WizardSteps>
                        <asp:WizardStep Title="Create Account" runat="server" ID="wsUsernameAndPassword">
                            <div class="divContainerRow">
                                <div class="divContainerTitle">
                                    Creating an account with us is a quick process!  Let's get started by creating your login.
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
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
                                        ID="RegularExpressionValidator2" 
                                        runat="server" 
                                        ForeColor="Red"
                                        ErrorMessage="This does not appear to be a valid email address!" 
                                        ControlToValidate="txtEmail"     
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Username:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="valRequiredUsername" 
                                        runat="server" 
                                        ForeColor="Red" 
                                        ControlToValidate="txtUsername" 
                                        ErrorMessage="Please provide a username!">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator 
                                        ID="valUsernameValidation" 
                                        runat="server" 
                                        ForeColor="Red"
                                        ErrorMessage="Your username must be at least 6 letters or numbers and no more than 30." 
                                        ControlToValidate="txtUsername"     
                                        ValidationExpression="^[a-zA-Z0-9.]{6,30}">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Password:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator1" 
                                        runat="server" 
                                        ForeColor="Red"
                                        ControlToValidate="txtPassword"
                                        ValidationExpression="(?=^.{5,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"
                                        Display="Dynamic"
                                        ErrorMessage="Your password must be at least 8 characters long and contain at
                                         least one upper case letter, one lower case letter, one number, and one special character">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Verify Password:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtVerifyPassword" TextMode="Password" runat="server"></asp:TextBox>
                                    <asp:CompareValidator 
                                        ID="valComparePasswords" 
                                        runat="server" 
                                        ForeColor="Red"
                                        ControlToValidate="txtPassword" 
                                        ControlToCompare="txtVerifyPassword" 
                                        ErrorMessage="The passwords you entered do no match!" 
                                        Display="Dynamic">*</asp:CompareValidator>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep Title="About You" runat="server" ID="wsWhoYouAre">
                            <div class="divContainerRow">
                                <div class="divContainerTitle">
                                    Tell us a little bit about yourself!
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    First Name:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="valRequireFirstName" 
                                        runat="server" 
                                        ForeColor="Red" 
                                        ControlToValidate="txtFirstName" 
                                        ErrorMessage="Please provide your first name!">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Last Name:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="valRequiredLastName" 
                                        runat="server" 
                                        ForeColor="Red" 
                                        ControlToValidate="txtLastName" 
                                        ErrorMessage="Please provide your last name!">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Birthday:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtBirthday" runat="server" Text=""></asp:TextBox>
                                    <asp:CompareValidator 
                                        ID="valDate" 
                                        runat="server" 
                                        ForeColor="Red" 
                                        ControlToValidate="txtBirthday" 
                                        Type="Date" 
                                        Operator="DataTypeCheck"
                                        ErrorMessage="Please enter a valid date!">*</asp:CompareValidator>
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell divContainerCellHeader">
                                    Zipcode:
                                </div>
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtZipcode" runat="server" Text=""></asp:TextBox>
                                    <asp:RegularExpressionValidator 
                                        ID="valZipcode" ControlToValidate="txtZipcode"
                                        runat="server" 
                                        ForeColor="Red" 
                                        ErrorMessage="This must be a valid US zip code!" 
                                        ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep>
                            <div class="divContainerRow">
                                <div class="divContainerCell">
                                    <asp:TextBox TextMode="MultiLine" Columns="40" Rows="10" ID="txtTerms" runat="server"></asp:TextBox><br />
                                    <asp:CheckBox ID="chkAgreeWithTerms" runat="server" Text="I agree with the terms" />
                                    <asp:Label ID="lblTermID" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep Title="CAPTCHA" ID="wsCaptcha" runat="server">
                            <div class="divContainerRow">
                                <div class="divContainerTitle">
                                    CAPTCHA - Completely Automated Turing Test To Tell Computers and Humans Apart
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell">
                                    <asp:Image runat="server" ImageUrl="~/images/CaptchaImage/JpegImage.aspx" />
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerTitle">
                                    Please copy what you see in the image above into the box below.
                                </div>
                            </div>
                            <div class="divContainerRow">
                                <div class="divContainerCell">
                                    <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </asp:WizardStep>
                    </WizardSteps>    
                </asp:Wizard>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAccountCreated" Visible="false" runat="server">
        <div class="divContainer>
            <div class="divContainerRow">
                <div class="divContainerCell">
                    Your account was created successfully.<br /><br />
                    <asp:Label runat="server" ForeColor="Red" Text="Email verification required!"></asp:Label><br />There is one step left.  Please go to your email account and open the 
                    verification email we just sent to you.  There you will find a link that you must follow to verify your email address.  Once this step has been completed you can
                    log in.<br /><br />
                    Thank you for signing up!<br /><br />
                    <asp:LinkButton ID="lbLogin" runat="server" Text="Click here to login!" OnClick="lbLogin_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>