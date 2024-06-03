<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MyPosts.aspx.cs" Inherits="Fisharoo.FisharooWeb.Blogs.MyPosts" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <table width="100%"><tr><td>
                    <asp:ListView ID="lvBlogs" runat="server" OnItemDataBound="lvBlogs_ItemDataBound">
                        <LayoutTemplate>
                            <ul class="blogsList">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        
                        <ItemTemplate>
                            <li>
                                <h2 class="blogsTitle"><asp:HyperLink ID="linkTitle" runat="server" Text='<%#((Blog)Container.DataItem).Title %>'></asp:HyperLink></h2>
                                <p class="blogsDescription">
                                <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CssClass="blogsActionLink" OnClick="lbEdit_Click"></asp:LinkButton> - 
                                <asp:LinkButton ID="lbDelete" runat="server" Text="Delete" CssClass="blogsActionLink" OnClick="lbDelete_Click"></asp:LinkButton><br />
                                Created: <%#((Blog)Container.DataItem).CreateDate %> By: <%#((Blog)Container.DataItem).Account.Username %><br />
                                <%#((Blog)Container.DataItem).Subject %><asp:Literal Visible="false" ID="litBlogID" runat="server" Text='<%#((Blog)Container.DataItem).BlogID %>'></asp:Literal>
                                <asp:Literal ID="litPageName" runat="server" Visible="false" Text='<%#((Blog)Container.DataItem).PageName %>'></asp:Literal>
                                <asp:Literal ID="litUsername" runat="server" Visible="false" Text='<%#((Blog)Container.DataItem).Account.Username %>'></asp:Literal>
                                </p>
                            </li>
                        </ItemTemplate>
                        
                        <EmptyDataTemplate>
                            Sorry, there are no blogs posted yet!
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td></tr></table>
            </div>
        </div>
    </div>     
</asp:Content>