<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewAlbum.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.ViewAlbum" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain" %>
<%@ Register Src="~/UserControls/Ratings.ascx" TagName="Ratings" TagPrefix="Fisharoo" %>
<%@ Register Src="~/UserControls/Tags.ascx" TagName="Tags" TagPrefix="Fisharoo" %>
<%@ Register Src="~/UserControls/Comments.ascx" TagName="Comments" TagPrefix="Fisharoo" %>
<%@ Register Src="~/UserControls/Moderations.ascx" TagName="Moderations" TagPrefix="Fisharoo" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle"><asp:Label ID="lblAlbumName" runat="server"></asp:Label></div>
            <div class="divContainerRow">
                Created: <asp:Label ID="lblCreateDate" runat="server"></asp:Label><br />
                Location: <asp:Label ID="lblLocation" runat="server"></asp:Label><br />
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </div>
            <div class="divContainerRow">
                <table width="100%"><tr><td>
                    <asp:ListView ID="lvGallery" runat="server" OnItemDataBound="lvAlbum_ItemDataBound">
                        <LayoutTemplate>
                            <ul class="albumsList">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <Fisharoo:Moderations ID="Moderations1" SystemObjectID="5" SystemObjectRecordID='<%#((File)Container.DataItem).FileID %>' runat="server"></Fisharoo:Moderations>
                                <asp:Label style="font-weight:bold;" ID="lblFileName" Text='<%#((File)Container.DataItem).FileName %>' runat="server"></asp:Label>
                                <asp:HyperLink ID="linkImage" NavigateUrl='<%#((File)Container.DataItem).CreateDate.Year.ToString() + ((File)Container.DataItem).CreateDate.Month.ToString() %>' runat="server"></asp:HyperLink>
                                <asp:Literal Visible="false" ID="litImageName" runat="server" Text='<%#((File)Container.DataItem).FileSystemName.ToString() %>'></asp:Literal>
                                <asp:Literal Visible="false" ID="litFileExtension" runat="server" Text='<%# ((File)Container.DataItem).Extension.ToString() %>'></asp:Literal><br />
                                <asp:Label ID="lblDescription" runat="server" Text='<%#((File)Container.DataItem).Description %>'></asp:Label>
                                <asp:Literal Visible="false" ID="litFileID" Text='<%#((File)Container.DataItem).FileID %>' runat="server"></asp:Literal>
                                <Fisharoo:Ratings ID="Ratings1" runat="server" SystemObjectID="5" SystemObjectRecordID='<%#((File)Container.DataItem).FileID %>'></Fisharoo:Ratings>
                                <Fisharoo:Tags ID="Tags1" runat="server" SystemObjectID="5" SystemObjectRecordID='<%#((File)Container.DataItem).FileID %>' Display="ShowCloudAndTagBox" ></Fisharoo:Tags>
                                <Fisharoo:Comments ID="Comments1" runat="server" SystemObjectID="5" SystemObjectRecordID='<%#((File)Container.DataItem).FileID %>'></Fisharoo:Comments>
                            </li>
                        </ItemTemplate>
                        <EmptyItemTemplate>
                            There are no photos in this gallery!  
                            <asp:HyperLink ID="linkAddPhotos" runat="server" Text="Click here to add photos"></asp:HyperLink>.
                        </EmptyItemTemplate>
                    </asp:ListView>
                </td></tr></table>
            </div>
            <div class="divContainerFooter">
                <asp:Button ID="btnAddPhotos" runat="server" Text="Add Photos" OnClick="btnAddPhotos_Click" /> 
                <asp:Button ID="btnEditPhotos" runat="server" Text="Edit Photos" OnClick="lbEditPhotos_Click"></asp:Button> 
                <asp:Button ID="btnEditAlbum" runat="server" Text="Edit Album" OnClick="lbEditAlbum_Click"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>