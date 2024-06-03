<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Forum.Default" %>
<%@ Import Namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer">
    <div class="divContainerBox">
        <div class="divContainerRow">
            <asp:Repeater ID="repCategories" runat="server" OnItemDataBound="repCategories_ItemDataBound">
                <HeaderTemplate>
                    <table width="100%">
                        <tr style="background-color:#bbbbbb;font-weight:bold;">
                            <td>Title</td>
                            <td>Subject</td>
                            <td>Threads</td>
                            <td>Posts</td>
                            <td>Last Post by</td>
                            <td>Last Post on</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color:#dddddd;font-weight:bold;">
                        <td><%# ((BoardCategory)Container.DataItem).Name %></td>
                        <td><%# ((BoardCategory)Container.DataItem).Subject %></td>
                        <td><%# ((BoardCategory)Container.DataItem).ThreadCount %></td>
                        <td><%# ((BoardCategory)Container.DataItem).PostCount %></td>
                        <td><%# ((BoardCategory)Container.DataItem).LastPostByUsername %></td>
                        <td><%# ((BoardCategory)Container.DataItem).LastPostDate.ToShortDateString() %></td>
                    </tr>
                    <asp:Repeater ID="repForums" runat="server" OnItemDataBound="repForums_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbForum" runat="server" Text='<%# ((BoardForum)Container.DataItem).Name %>' OnClick="lbForum_Click"></asp:LinkButton>
                                    <asp:Literal ID="litPageName" runat="server" Text='<%# ((BoardForum)Container.DataItem).PageName%>' Visible="false"></asp:Literal>
                                </td>
                                <td><%# ((BoardForum)Container.DataItem).Subject%></td>
                                <td><%# ((BoardForum)Container.DataItem).ThreadCount%></td>
                                <td><%# ((BoardForum)Container.DataItem).PostCount%></td>
                                <td><%# ((BoardForum)Container.DataItem).LastPostByUsername%></td>
                                <td><%# ((BoardForum)Container.DataItem).LastPostDate.ToShortDateString() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
</asp:Content>