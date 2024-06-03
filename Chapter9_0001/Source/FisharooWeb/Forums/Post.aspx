<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="Fisharoo.FisharooWeb.Forums.Post" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <div class="divContainerCell"><asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></div>
                <div class="divContainerCellHeader">Name:</div>
                <div class="divContainerCell"><asp:TextBox Width="400" ID="txtName" runat="server"></asp:TextBox></div>
                <div class="divContainerCellHeader">Page Name:</div>
                <div class="divContainerCell"><asp:TextBox Width="400" ID="txtPageName" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <asp:TextBox ID="txtPost" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="divContainerFooter"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></div>
        </div>
    </div>
    
    <script type="text/javascript">
        xinha_editors[xinha_editors.length] = 'ctl00_Content_txtPost';
    </script>
</asp:Content>