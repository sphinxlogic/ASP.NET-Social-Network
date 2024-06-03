<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestUpload.aspx.cs" Inherits="Galleries_TestUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form method="post" action="ReceiveFiles.aspx?AccountID=1&AlbumID=1&FileType=1" enctype="multipart/form-data">
    <div>
        <input type="file" class="stdInput" id="file2" runat="server" NAME="file2"/>
    </div>
    <input type="submit" value="test" />
    </form>
</body>
</html>
