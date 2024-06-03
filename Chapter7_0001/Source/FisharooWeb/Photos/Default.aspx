<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.Default" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">    
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <table width="100%"><tr><td>
                <asp:ListView id="lvAlbums" runat="server" OnItemDataBound="lbAlbums_ItemDataBound">
                    <LayoutTemplate>
                        <ul class="albumsList">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    
                    <ItemTemplate>
                        <li>
                            <asp:Label CssClass="albumsTitle" ID="lblName" Text='<%#((Folder)Container.DataItem).Name %>' runat="server"></asp:Label><br />
                            <asp:HyperLink CssClass="albumsAuthor" ID="linkAuthor" Text='<%#((Folder)Container.DataItem).Username %>' runat="server"></asp:HyperLink><br />
                            <asp:HyperLink  ID="linkGallery" runat="server" /><br />
                            <asp:Label ID="Label1" CssClass="albumsLocation" Text="in - " runat="server"></asp:Label>
                            <asp:Label CssClass="albumsLocation" ID="lblLocation" Text='<%#((Folder)Container.DataItem).Location %>' runat="server"></asp:Label><br />
                            <asp:Label CssClass="albumsDescription" ID="lblDescription" Text='<%#((Folder)Container.DataItem).Description %>' runat="server"></asp:Label>
                            <asp:Literal Visible="false" ID="litFolderID" Text='<%#((Folder)Container.DataItem).FolderID.ToString() %>' runat="server"></asp:Literal>
                            <asp:Literal Visible="false" ID="litFullPath" Text='<%#((Folder)Container.DataItem).FullPathToCoverImage %>' runat="server"></asp:Literal>
                        </li>
                    </ItemTemplate>
                    
                    <EmptyDataTemplate>
                        Sorry, your friends do not have any photo albums!
                    </EmptyDataTemplate>
                </asp:ListView>
                </td></tr></table>
            </div>
        </div>
    </div>
</asp:Content>