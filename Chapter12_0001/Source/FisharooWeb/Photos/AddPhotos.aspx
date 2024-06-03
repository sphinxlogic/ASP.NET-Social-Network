<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AddPhotos.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.AddPhotos" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>


<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Upload some images to your album.</div>
            <div class="divContainerRow" style="text-align:center;">
                <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="550" height="400" id="FileUpload" align="middle">
                    <param name="allowScriptAccess" value="sameDomain" />
                    <param name="movie" value="../Files/FileUpload.swf?SiteRoot=<%Response.Write(_webContext.RootUrl);%>&AlbumID=<%Response.Write(_webContext.AlbumID.ToString()); %>&FileType=<%Response.Write(((int)Folder.Types.Picture).ToString()); %>&AccountID=<%Response.Write(_webContext.CurrentUser.AccountID.ToString()); %>" />
                    <param name="quality" value="high" />
                    <param name="bgcolor" value="#ffffff" />
                    <embed src="../Files/FileUpload.swf?SiteRoot=<%Response.Write(_webContext.RootUrl);%>&AlbumID=<%Response.Write(_webContext.AlbumID.ToString()); %>&FileType=<%Response.Write(((int)Folder.Types.Picture).ToString()); %>&AccountID=<%Response.Write(_webContext.CurrentUser.AccountID.ToString()); %>"
                        quality="high" 
                        bgcolor="#ffffff" 
                        width="550" 
                        height="220" 
                        name="FileUpload" 
                        align="middle" 
                        allowScriptAccess="sameDomain" 
                        type="application/x-shockwave-flash" 
                        pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
            </div>
            <div class="divContainerRow">
                Upload as many pictures as you like.  One directory at a time.  
                When you are done uploading all of your files you can either
                <asp:LinkButton ID="lbViewAlbum" Text="view your album" OnClick="lbViewAlbum_Click" runat="server"></asp:LinkButton>
                 or 
                <asp:LinkButton ID="lbEditPhotos" Text="add details to your images" OnClick="lbEditPhotos_Click" runat="server"></asp:LinkButton>
                .
            </div>
        </div>
    </div>
</asp:Content>
