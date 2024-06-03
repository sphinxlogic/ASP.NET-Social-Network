<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ManageProfile.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.ManageProfile" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>  
        <asp:Label ID="lblProfileID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblProfileTimestamp" runat="server" Visible="false"></asp:Label>
        <asp:Wizard ID="wizProfile" runat="server" OnNextButtonClick="wizProfile_NextButtonClick" OnFinishButtonClick="wizProfile_FinishButtonClicked" SideBarStyle-CssClass="WizardSideBar" CssClass="Wizard"  >
            <HeaderTemplate>
                <asp:ValidationSummary ID="ErrorSummary" runat="server" ForeColor="Red" DisplayMode="BulletList" />
            </HeaderTemplate>
            <WizardSteps>
                <asp:WizardStep Title="Tank Info" ID="wsTankInfo" runat="server">
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Year of first tank:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtYearOfFirstTank" runat="server"></asp:TextBox>
                            <asp:RangeValidator 
                                runat="server"
                                ForeColor="Red" 
                                ControlToValidate="txtYearOfFirstTank" 
                                MinimumValue="1900" 
                                MaximumValue="2020" 
                                Display="Dynamic" 
                                ErrorMessage="Please enter the 4 digit year that you started your first tank!">*</asp:RangeValidator>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Number of tanks owned:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtNumberOfTanksOwned" runat="server"></asp:TextBox>
                            <asp:CompareValidator
                                runat="server"
                                ForeColor="Red"
                                Operator="DataTypeCheck"
                                ControlToValidate="txtNumberOfTanksOwned" 
                                Type="Integer" 
                                ErrorMessage="Must be a number">*</asp:CompareValidator>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Number of fish owned:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtNumberOfFishOwned" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1"
                                runat="server"
                                ForeColor="Red" 
                                Operator="DataTypeCheck"
                                ControlToValidate="txtNumberOfFishOwned" 
                                Type="Integer" 
                                ErrorMessage="Must be a number">*</asp:CompareValidator>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Level of experience:
                        </div>
                        <div class="divContainerCell">
                            <asp:DropDownList ID="ddlLevelOfExperience" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep Title="Signature" ID="wsSignature" runat="server">
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Your signature:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtSignature" TextMode="MultiLine" Columns="20" Rows="4" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep Title="Instant Messaging" ID="wsInstantMessaging" runat="server">
                    <div class="divContainerTitle">
                        Instant Messaging Services
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            MSN:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMMSN" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            AOL:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMAOL" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Google IM:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMGIM" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Yahoo IM:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMYIM" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            ICQ #:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMICQ" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divContainerRow">
                        <div class="divContainerCellHeader">
                            Skype:
                        </div>
                        <div class="divContainerCell">
                            <asp:TextBox ID="txtIMSkype" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </asp:WizardStep>
                <asp:WizardStep Title="About You" ID="wsAttributes" runat="server">
                    <div class="divContainerTitle">
                        All about you
                    </div>
                    <asp:PlaceHolder ID="phAttributes" runat="server"></asp:PlaceHolder>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
    
    <script type="text/javascript">
    function MaxLength2000(sender, args)
      {
        if(args.Value.length > 2000)
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = true;
      }
        
    </script>
</asp:Content>