<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="EditAlbum.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.EditAlbum" %>


<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="divContainer">
        <div class="divContainerBox">
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Name:</div>
                <div class="divContainerCell"><asp:TextBox ID="txtFolderName" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Location:</div>
                <div class="divContainerCell"><asp:TextBox ID="txtLocation" runat="server"></asp:TextBox></div>
            </div>
            <div class="divContainerRow">
                <div class="divContainerCellHeader">Description:</div>
                <div class="divContainerCell">
                    <asp:TextBox Columns="30" Rows="4" ID="txtDescription" MaxLength="500" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        ID="txtConclusionValidator1" 
                        ControlToValidate="txtDescription" 
                        Text="500 character limit!" 
                        ValidationExpression="^[\s\S]{0,500}$" 
                        runat="server" />
                </div>
            </div>
            <div class="divContainerFooter">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Literal ID="litFolderID" runat="server" Visible="false"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
