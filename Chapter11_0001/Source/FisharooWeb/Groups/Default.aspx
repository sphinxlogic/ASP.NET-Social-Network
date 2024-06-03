<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fisharoo.FisharooWeb.Groups.Default" %>

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
                            <asp:LinkButton OnClick="lbPageName_Click" id="lbPageName" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).Name %>'></asp:LinkButton>
                            <asp:Image ID="imgGroupImage" runat="server" />
                            <asp:Label ID="lblName" runat="server" Text='<%# ((Fisharoo.FisharooCore.Core.Domain.Group)Container.DataItem).Name %>'></asp:Label>
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