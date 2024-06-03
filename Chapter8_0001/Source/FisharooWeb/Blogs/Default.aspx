<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Blogs.Default" %>
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
                                Created: <%#((Blog)Container.DataItem).CreateDate %> By: <%#((Blog)Container.DataItem).Account.Username %><br />
                                <%#((Blog)Container.DataItem).Subject %><asp:Literal ID="litBlogID" runat="server" Text='<%#((Blog)Container.DataItem).BlogID %>'></asp:Literal>
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