<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewForum.aspx.cs" Inherits="Fisharoo.FisharooWeb.Forums.Forum" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer">
    <div class="divContainerBox">
        <div class="divContainerRow">
            <asp:Literal ID="litCategoryPageName" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="litForumPageName" runat="server" Visible="false"></asp:Literal>
            <table width="100%" cellpadding="5" cellspacing="0">
                        <tr style="background-color:#bbbbbb;font-weight:bold;">
                            <td colspan="2">By</td>
                            <td>On</td>
                            <td>Updated</td>
                            <td>Views</td>
                            <td>Replies</td>
                            <td colspan="1">Last Reply By</td>
                            <td><asp:HyperLink ID="linkNewThread" runat="server" Text="New Thread"></asp:HyperLink></td>
                        </tr>
            <asp:Repeater ID="repTopics" runat="server" OnItemDataBound="repTopics_ItemDataBound">
                <ItemTemplate>
                        <tr style="background-color:#dddddd;">
                            <td colspan="8">
                                <asp:HyperLink ID="linkViewTopic" runat="server" Text='<%#((BoardPost)Container.DataItem).Name %>'></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color:#dddddd;">
                            <td><asp:HyperLink ID="linkUsername" runat="server" Text='<%#((BoardPost)Container.DataItem).Username %>' NavigateUrl='<%# "/" + ((BoardPost)Container.DataItem).Username %>'></asp:HyperLink></td>
                            <td><asp:Image Width="100" Height="100" ID="Image2" ImageUrl='<%# "/images/ProfileAvatar/ProfileImage.aspx?AccountID=" + ((BoardPost)Container.DataItem).AccountID %>' runat="server" /></td>
                            <td><%#((BoardPost)Container.DataItem).CreateDate.ToShortDateString() %></td>
                            <td><%#((BoardPost)Container.DataItem).UpdateDate.ToShortDateString() %></td>
                            <td><%#((BoardPost)Container.DataItem).ViewCount %></td>
                            <td><%#((BoardPost)Container.DataItem).ReplyCount %></td>
                            <td><asp:HyperLink ID="linkReplyUsername" runat="server" Text='<%#((BoardPost)Container.DataItem).ReplyByUsername %>' NavigateUrl=' <%# "/" + ((BoardPost)Container.DataItem).ReplyByUsername %>'></asp:HyperLink></td>
                            <td><asp:Image Width="100" Height="100" ID="Image1" ImageUrl='<%# "/images/ProfileAvatar/ProfileImage.aspx?AccountID=" + ((BoardPost)Container.DataItem).ReplyByAccountID %>' runat="server" /></td>
                        </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                        <tr style="background-color:#ffffff;">
                            <td colspan="8">
                                <asp:HyperLink ID="linkViewTopic" runat="server" Text='<%#((BoardPost)Container.DataItem).Name %>'></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="background-color:#ffffff;">
                            <td><asp:HyperLink ID="linkUsername" runat="server" Text='<%#((BoardPost)Container.DataItem).Username %>' NavigateUrl='<%# "/" + ((BoardPost)Container.DataItem).Username %>'></asp:HyperLink></td>
                            <td><asp:Image Width="100" Height="100" ID="Image2" ImageUrl='<%# "/images/ProfileAvatar/ProfileImage.aspx?AccountID=" + ((BoardPost)Container.DataItem).AccountID %>' runat="server" /></td>
                            <td><%#((BoardPost)Container.DataItem).CreateDate.ToShortDateString()%></td>
                            <td><%#((BoardPost)Container.DataItem).UpdateDate.ToShortDateString()%></td>
                            <td><%#((BoardPost)Container.DataItem).ViewCount %></td>
                            <td><%#((BoardPost)Container.DataItem).ReplyCount %></td>
                            <td><asp:HyperLink ID="linkReplyUsername" runat="server" Text='<%#((BoardPost)Container.DataItem).ReplyByUsername %>' NavigateUrl=' <%# "/" + ((BoardPost)Container.DataItem).ReplyByUsername %>'></asp:HyperLink></td>
                            <td><asp:Image Width="100" Height="100" ImageUrl='<%# "/images/ProfileAvatar/ProfileImage.aspx?AccountID=" + ((BoardPost)Container.DataItem).ReplyByAccountID %>' runat="server" /></td>
                        </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
        </div>
    </div>
</div>
</asp:Content>