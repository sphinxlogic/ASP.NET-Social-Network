<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="Fisharoo.FisharooWeb.Blogs.Post" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">&nbsp;<asp:Label ForeColor="Red" ID="lblErrorMessage" runat="server"></asp:Label></div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Title:</div>
                <div class="divContainerCell"><asp:TextBox Width="300" ID="txtTitle" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Subject:</div>
                <div class="divContainerCell"><asp:TextBox Width="300" ID="txtSubject" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Page Name:</div>
                <div class="divContainerCell"><asp:TextBox Width="300" ID="txtPageName" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Is Published:</div>
                <div class="divContainerCell"><asp:CheckBox ID="chkIsPublished" runat="server"></asp:CheckBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCell" style="padding-left:20px;">
                    <asp:TextBox TextMode="MultiLine" ID="txtPost" runat="server"></asp:TextBox>
                    <asp:Literal ID="litBlogID" runat="server" Visible="false"></asp:Literal>
                </div>
            </div>
            <div class="divContainerFooter">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        xinha_editors[xinha_editors.length] = 'ctl00_Content_txtPost';
    </script>
    
</asp:Content>