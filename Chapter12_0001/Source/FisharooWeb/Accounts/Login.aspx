<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Fisharoo.FisharooWeb.Accounts.Login" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer">
    <div class="divContainerBox">
        <asp:Panel DefaultButton="btnLogin" runat="server">
            <div class="divContainerRow">
                <div class="divContainerTitle">
                    Please log in.
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Username:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    Password:
                </div>
                <div class="divContainerCell">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    &nbsp;
                </div>
                <div class="divContainerCell">
                    <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Log In" /><br />
                    <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">
                    &nbsp;<br />
                    &nbsp;<br />
                    &nbsp;<br />
                </div>
                <div class="divContainerCell">
                    <asp:LinkButton ID="lbRecoverPassword" runat="server" Text="Forgot Password?" OnClick="lbRecoverPassword_Click"></asp:LinkButton><br />
                    <asp:LinkButton ID="lbRegister" runat="server" Text="Register" OnClick="lbRegister_Click"></asp:LinkButton><br />
                    &nbsp;
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>