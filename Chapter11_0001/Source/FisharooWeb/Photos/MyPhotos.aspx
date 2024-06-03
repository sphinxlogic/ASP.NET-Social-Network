<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MyPhotos.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.MyPhotos" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Here are your photo albums!</div>
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
                            <asp:HyperLink CssClass="albumsActionLink" ID="linkEditAlbum" NavigateUrl="~/Photos/EditAlbum.aspx" Text="Edit" runat="server"></asp:HyperLink> 
                            <asp:HyperLink CssClass="albumsActionLink" ID="linkViewAlbum" NavigateUrl="~/Photos/ViewAlbum.aspx" Text="View" runat="server"></asp:HyperLink> 
                            <asp:LinkButton CssClass="albumsActionLink" ID="linkDeleteAlbum" Text="Delete" OnClick="linkDeleteAlbum_Click" runat="server"></asp:LinkButton><br />
                            <asp:Label CssClass="albumsTitle" ID="lblName" Text='<%#((Folder)Container.DataItem).Name %>' runat="server"></asp:Label><br />
                            <img src="<%#_webContext.RootUrl %>files/photos/<%#((Folder)Container.DataItem).FullPathToCoverImage %>" /><br />
                            <asp:Label CssClass="albumsLocation" Text="in - " runat="server"></asp:Label>
                            <asp:Label CssClass="albumsLocation" ID="lblLocation" Text='<%#((Folder)Container.DataItem).Location %>' runat="server"></asp:Label><br />
                            <asp:Label CssClass="albumsDescription" ID="lblDescription" Text='<%#((Folder)Container.DataItem).Description %>' runat="server"></asp:Label>
                            <asp:Literal Visible="false" ID="litFolderID" Text='<%#((Folder)Container.DataItem).FolderID.ToString() %>' runat="server"></asp:Literal>
                        </li>
                    </ItemTemplate>
                    
                    <EmptyDataTemplate>
                        Sorry, you don't seem to have any albums at this time!
                    </EmptyDataTemplate>
                </asp:ListView>
                </td></tr></table>
            </div>
        </div>
    </div>
</asp:Content>
