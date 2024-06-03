<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="Fisharoo.FisharooWeb.Accounts.RecoverPassword" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <asp:Panel ID="pnlRecoverPassword" runat="server">
        <div class="divContainer">
            <div class="divContainerRow">
                <div class="divContainerTitle">
                    Please enter your email address below
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Email:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    &nbsp;
                </div>
                <div class="divContainerCell">
                    <asp:Button ID="btnRecoverPassword" Text="Recover Password" runat="server" OnClick="btnRecoverPassword_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="divContainer">
        <div class="divContainerRow">
            <div class="divContainerCell">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>