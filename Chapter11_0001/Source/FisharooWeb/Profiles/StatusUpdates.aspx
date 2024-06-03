<%@ Page MasterPageFile="~/SiteMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="StatusUpdates.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.StatusUpdates" %>
<%@ Import namespace="Fisharoo.FisharooCore.Core.Domain"%>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerTitle">Status Updates</div>
            <div class="divContainerCell">
                <asp:Repeater ID="repStatusUpdates" runat="server">
                    <ItemTemplate>
                        <%# ((StatusUpdate)Container.DataItem).CreateDate.ToString() %> - 
                        <%# ((StatusUpdate)Container.DataItem).Status %>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <div class="divContainerSeparator"></div>
                    </SeparatorTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>