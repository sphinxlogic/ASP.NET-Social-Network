<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MyGroups.aspx.cs" Inherits="Fisharoo.FisharooWeb.Groups.MyGroups" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <table cellpadding="0" cellspacing="0" width="100%"><tr><td>
                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                <asp:ListView id="lvGroups" runat="server" OnItemDataBound="lvGroups_ItemDataBound">
                    <LayoutTemplate>
                        <ul class="groupsList">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    
                    <ItemTemplate>
                        <li>
                            <asp:Literal Visible="false" ID="litImageID" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).FileID %>'></asp:Literal>
                            <asp:Literal ID="litPageName" Visible="false" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).PageName %>'></asp:Literal>
                            <div>
                                <div style="float:left;"><asp:LinkButton OnClick="lbPageName_Click" id="lbPageName" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).Name %>'></asp:LinkButton></div>
                                <div style="text-align:right;"><asp:ImageButton ID="ibDelete" OnClick="ibDelete_Click" runat="server" ImageUrl="~/images/icon_close.gif" />
                                <asp:ImageButton ID="ibEdit" OnClick="ibEdit_Click" runat="server" ImageUrl="~/images/icon_pencil.gif" /></div>
                            </div>                                                        
                            <asp:Image ID="imgGroupImage" runat="server" />
                            <asp:Label ID="lblName" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).Name %>'></asp:Label>
                            <asp:Literal Visible="false" ID="litGroupID" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).GroupID %>'></asp:Literal>
                        </li>
                    </ItemTemplate>
                    
                    <EmptyDataTemplate>
                        
                    </EmptyDataTemplate>
                </asp:ListView>
                </td></tr></table>
            </div>
        </div>
    </div>
</asp:Content>