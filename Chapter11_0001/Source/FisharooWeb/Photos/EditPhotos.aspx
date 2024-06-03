<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="EditPhotos.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.EditPhotos" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>


<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Here are your photos!</div>
            <div class="divContainerRow">
                <table width="100%"><tr><td>
                <asp:ListView id="lvAlbums" runat="server" OnItemDataBound="lbPhotos_ItemDataBound">
                    <LayoutTemplate>
                        <ul class="photosList">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    
                    <ItemTemplate>
                        <li>
                            <asp:Label style="font-weight:bold;" id="lblFileName" runat="server" Text='<%#((File)Container.DataItem).FileName %>'></asp:Label><br />                            
                            <table width="300" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        Description:<br />
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Columns="10" Rows="7" runat="server" Text='<%#((File)Container.DataItem).Description %>'></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:HyperLink ID="linkImage" runat="server" NavigateUrl='<%#((File)Container.DataItem).CreateDate.Year.ToString() + ((File)Container.DataItem).CreateDate.Month.ToString() %>'></asp:HyperLink>
                                        <asp:Literal Visible="false" ID="litFileSystemName" runat="server" Text='<%#((File)Container.DataItem).FileSystemName.ToString() %>'></asp:Literal>
                                        <asp:Literal Visible="false" ID="litFileID" runat="server" Text='<%#((File)Container.DataItem).FileID.ToString() %>'></asp:Literal>
                                        <asp:Literal Visible="false" ID="litFileExtension" runat="server" Text='<%#((File)Container.DataItem).Extension %>'></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ItemTemplate>
                    
                    <EmptyDataTemplate>
                        Sorry, you don't seem to have any photos at this time!
                    </EmptyDataTemplate>
                </asp:ListView>
                </td></tr></table>
            </div>
            <div class="divContainerFooter">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /> 
                <asp:Button ID="btnBack" runat="server" Text="Back To Album View" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>