﻿<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Folders.aspx.cs" Inherits="Fisharoo.FisharooAdminConsole.Files.Folders" %>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:DetailsView 
        ID="dvFolder" 
        runat="server" 
        DataSourceID="FoldersDataSource" 
        AllowPaging="true" 
        AutoGenerateDeleteButton="true" 
        AutoGenerateEditButton="true" 
        AutoGenerateInsertButton="true" 
        DataKeyNames="FolderID">
        </asp:DetailsView> 
    <asp:GridView 
        ID="gvFolders" 
        runat="server" 
        DataSourceID="FoldersDataSource" 
        AllowPaging="true" 
        AllowSorting="true" 
        AutoGenerateDeleteButton="true" 
        AutoGenerateEditButton="true"></asp:GridView>
    <asp:LinqDataSource 
        ID="FoldersDataSource" 
        ContextTypeName="Fisharoo.FisharooCore.Core.Domain.FisharooDataContext" 
        TableName="Folders" 
        EnableDelete="true" 
        EnableInsert="true" 
        EnableUpdate="true" 
        runat="server"></asp:LinqDataSource>
</asp:Content>