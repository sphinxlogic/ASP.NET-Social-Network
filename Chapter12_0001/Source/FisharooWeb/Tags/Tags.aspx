﻿<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Fisharoo.FisharooWeb.Tags.Tags" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <table width="100%">
                    <tr>
                        <td colspan="2"><b>Accounts</b></td>
                    </tr>
                    <asp:Repeater ID="repAccounts" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td colspan="2"><b>Profiles</b></td>
                    </tr>
                    <asp:Repeater ID="repProfiles" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td colspan="2"><b>Blogs</b></td>
                    </tr>
                    <asp:Repeater ID="repBlogs" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td colspan="2"><b>Posts</b></td>
                    </tr>
                    <asp:Repeater ID="repPosts" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td colspan="2"><b>Files</b></td>
                    </tr>
                    <asp:Repeater ID="repFiles" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#((SystemObjectTagWithObjects)Container.DataItem).File.FileName %></td>
                                <td>
                                    <asp:HyperLink runat="server" Text='<%# "Click to view album: " + ((SystemObjectTagWithObjects)Container.DataItem).Folder.Name %>' NavigateUrl='<%# "~/photos/ViewAlbum.aspx?AlbumID=" + ((SystemObjectTagWithObjects)Container.DataItem).File.DefaultFolderID %>'></asp:HyperLink> or 
                                    <asp:HyperLink runat="server" Text='<%# "Click to view photo: " + ((SystemObjectTagWithObjects)Container.DataItem).File.FileName %>' NavigateUrl='<%# "~/files/photos/" + ((SystemObjectTagWithObjects)Container.DataItem).File.CreateDate.Year.ToString() + ((SystemObjectTagWithObjects)Container.DataItem).File.CreateDate.Month.ToString() + "/" + ((SystemObjectTagWithObjects)Container.DataItem).File.FileSystemName + "__O." + ((SystemObjectTagWithObjects)Container.DataItem).File.Extension %>'></asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <tr>
                        <td colspan="2"><b>Groups</b></td>
                    </tr>
                    <asp:Repeater ID="repGroups" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>