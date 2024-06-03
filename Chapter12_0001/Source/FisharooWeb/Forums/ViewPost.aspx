<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewPost.aspx.cs" Inherits="Fisharoo.FisharooWeb.Forums.ViewPost" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Impl" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <table cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="5" valign="top" style="font-weight:bold;font-size:16px;"><asp:Label ID="lblSubject" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td rowspan="2" valign="top" style="width:100px;"><asp:Image Width="100" Height="100" ID="imgProfile" runat="server" /></td>
                        <td style="height:12px;">by: <asp:HyperLink ID="linkUsername" runat="server"></asp:HyperLink></td>
                        <td>on: <asp:Label ID="lblCreateDate" runat="server"></asp:Label></td>
                        <td>updated: <asp:Label ID="lblUpdateDate" runat="server"></asp:Label></td>
                        <td><asp:HyperLink ID="linkReply" Text="Reply" runat="server"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td colspan="4" valign="top"><asp:Label ID="lblDescription" runat="server"></asp:Label></td>
                    </tr>
                    <asp:Repeater ID="repPosts" runat="server" OnItemDataBound="repPosts_ItemDataBound">
                        <ItemTemplate>
                            <tr style="background-color:#dddddd;">
                                <td colspan="5" style="font-weight:bold;font-size:16px;">
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#((BoardPost)Container.DataItem).Name.Filter() %>'></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color:#dddddd;">
                                <td rowspan="2" valign="top" style="width:100px;"><asp:Image Width="100" Height="100" ID="imgProfile" runat="server" ImageUrl='<%# "/images/profileavatar/profileimage.aspx?AccountID=" + ((BoardPost)Container.DataItem).AccountID %>' /></td>
                                <td style="height:12px;">by: <asp:HyperLink ID="linkUsername" NavigateUrl='<%# "/" + ((BoardPost)Container.DataItem).Username %>' runat="server" Text='<%#((BoardPost)Container.DataItem).Username %>'></asp:HyperLink></td>
                                <td>on: <asp:Label ID="lblCreateDate" runat="server" Text='<%#((BoardPost)Container.DataItem).CreateDate.ToShortDateString() %>'></asp:Label></td>
                                <td>updated: <asp:Label ID="lblUpdateDate" runat="server" Text='<%#((BoardPost)Container.DataItem).UpdateDate.ToShortDateString() %>'></asp:Label></td>
                                <td><asp:HyperLink runat="server" ID="linkReply" NavigateUrl='<%# "/forums/post.aspx?postID=" + ((BoardPost)Container.DataItem).PostID %>' Text="Reply"></asp:HyperLink></td>
                            </tr>
                            <tr style="background-color:#dddddd;">
                                <td colspan="4" valign="top"><asp:Label ID="lblDescription" runat="server" Text='<%#((BoardPost)Container.DataItem).Post.Filter() %>'></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td colspan="5" valign="top" style="font-weight:bold;font-size:16px;">
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#((BoardPost)Container.DataItem).Name.Filter() %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" valign="top" style="width:100px;"><asp:Image Width="100" Height="100" ID="imgProfile" runat="server" ImageUrl='<%# "/images/profileavatar/profileimage.aspx?AccountID=" + ((BoardPost)Container.DataItem).AccountID %>' /></td>
                                <td style="height:12px;">by: <asp:HyperLink ID="linkUsername" NavigateUrl='<%# "/" + ((BoardPost)Container.DataItem).Username %>' runat="server" Text='<%#((BoardPost)Container.DataItem).Username %>'></asp:HyperLink></td>
                                <td>on: <asp:Label ID="lblCreateDate" runat="server" Text='<%#((BoardPost)Container.DataItem).CreateDate.ToShortDateString() %>'></asp:Label></td>
                                <td>updated: <asp:Label ID="lblUpdateDate" runat="server" Text='<%#((BoardPost)Container.DataItem).UpdateDate.ToShortDateString() %>'></asp:Label></td>
                                <td><asp:HyperLink runat="server" ID="linkReply" NavigateUrl='<%# "/forums/post.aspx?postID=" + ((BoardPost)Container.DataItem).PostID %>' Text="Reply"></asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top"><asp:Label ID="lblDescription" runat="server" Text='<%#((BoardPost)Container.DataItem).Post.Filter() %>'></asp:Label></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>