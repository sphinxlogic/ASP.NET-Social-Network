<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewPhoto.aspx.cs" Inherits="Fisharoo.FisharooWeb.Photos.ViewPhoto" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
Previous Next
<asp:HyperLink NavigateUrl="~/Photos/ViewLargePhoto.aspx" Target="_blank" Text="Photo" runat="server"></asp:HyperLink>
</asp:Content>