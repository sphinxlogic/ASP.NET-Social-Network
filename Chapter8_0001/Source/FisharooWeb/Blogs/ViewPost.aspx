<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewPost.aspx.cs" Inherits="Fisharoo.FisharooWeb.Blogs.ViewPost" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <div class="divContainerCell" style="min-height:300px;">
                    <h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                    <asp:HyperLink ID="linkProfile" runat="server">
                    <asp:Image style="padding-bottom:5px;float:left;" Width="200" Height="200" ID="imgAvatar" runat="server" ImageUrl="/images/profileavatar/profileimage.aspx" />
                    </asp:HyperLink>
                    Created: <asp:Label ID="lblCreated" runat="server"></asp:Label>
                    Updated: <asp:Label ID="lblUpdated" runat="server"></asp:Label><br /><br />
                    <asp:Label ID="lblPost" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>