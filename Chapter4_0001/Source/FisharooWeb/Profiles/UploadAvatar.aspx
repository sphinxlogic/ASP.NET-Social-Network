<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UploadAvatar.aspx.cs" Inherits="Fisharoo.FisharooWeb.Profiles.UploadAvatar" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="/js/cropper/lib/prototype.js" language="javascript"></script>
<script type="text/javascript" src="/js/cropper/lib/scriptaculous.js?load=builder,dragdrop" language="javascript"></script>
<script type="text/javascript" src="/js/cropper/cropper.js" language="javascript"></script>
        <script type="text/javascript">
        function onEndCrop( coords, dimensions ) 
        {
            $( 'ctl00_Content_hidX1' ).value = coords.x1;
            $( 'ctl00_Content_hidY1' ).value = coords.y1;
            $( 'ctl00_Content_hidX2' ).value = coords.x2;
            $( 'ctl00_Content_hidY2' ).value = coords.y2;
            $( 'ctl00_Content_hidWidth' ).value = dimensions.width;
            $( 'ctl00_Content_hidHeight' ).value = dimensions.height;
        }
        </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
<div class="divContainer" style="min-height:120px">
    <div class="divContainerTitle">Use your Gravatar</div>
    <div class="divContainerRow">
        <div class="divContainerCellHeader"><asp:Image ImageUrl="~/images/ProfileAvatar/ProfileImage.aspx?ShowGravatar=1" runat="server" /></div>
        <div class="divContainerCell"><asp:CheckBox ID="cbUseGravatar" runat="server" Text="Yes, use my Gravatar" /></div>
    </div>
    <div class="divContainerRow">
        What is a Gravatar?  A Gravatar is a centrally located 
        avatar that you can take with you to participating sites, 
        communities, and blogs. 
        <asp:HyperLink NavigateUrl="http://www.gravatar.com" Text="http://www.gravatar.com" runat="server"></asp:HyperLink>
    </div>
    <div class="divContainerFooter">
        <asp:Button ID="btnUseGravatar" runat="server" OnClick="btnUseGravatar_Click" Text="Use Gravatar" />
    </div>
</div>
<br />
<asp:Panel ID="pnlUpload" runat="server" Visible="true">
    <div class="divContainer">
        <div class="divContainerTitle">Upload an avatar</div>
        <div class="divContainerRow">
            <div style="width:200px;height:200px;border:solid 2px #000000;float:left;background-color:#ffffff;">
                <div style="padding:10px;">
                    Avatars can be displayed as large as 200x200 pixels (the size of this box).
                    It is not suggested that you upload anything smaller than this as it 
                    might get stretched out a bit and not look as nice as you had hoped!
                </div>
            </div>
            <div>&nbsp;&nbsp;Avatar Path:<asp:FileUpload ID="fuAvatarUpload" runat="server" /></div>
        </div>
        <div class="divContainerFooter" style="height:185px;vertical-align:bottom">
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </div>
        <div class="divContainerRow">
            <asp:Label ForeColor="Red" ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlCrop" runat="server" Visible="false">
<asp:HiddenField ID="hidX1" runat="server" />
<asp:HiddenField ID="hidY1" runat="server" />
<asp:HiddenField ID="hidX2" runat="server" />
<asp:HiddenField ID="hidY2" runat="server" />
<asp:HiddenField ID="hidWidth" runat="server" />
<asp:HiddenField ID="hidHeight" runat="server" />
    
    <div class="divContainer">
        <div class="divContainerRow">
        <div class="divContainerRow">
            <div style="width:300px;height:100px;float:left;">
                <b>Now crop your avatar</b><BR />
                Images come in all shapes and sizes so we have provided you with a tool that 
                will allow you to select the section of your image that you would like to use for
                your avatar.  
            </div>
            <div id="previewWrap"></div>
        </div>
        <div><asp:Image ImageUrl="~/images/ProfileAvatar/ProfileImage.aspx" id="imgCropImage" runat="server"/></div>
        
<script type="text/javascript" language="javascript">
    Event.observe( window, 'load', function() {
        new Cropper.ImgWithPreview(
	        'ctl00_Content_imgCropImage',
	        { 
	            previewWrap: 'previewWrap',
	            minWidth: 100,
	            minHeight: 100,
	            ratioDim: {x: 100,y: 100},
		        displayOnInit: true,
		        onEndCrop: onEndCrop 
		    }
        );
    } );
</script>
        <div class="divContainerFooter">
            <asp:Button ID="btnCrop" Text="Perform Crop" runat="server" OnClick="btnCrop_Click" />
            <asp:Label ID="lblCropInfo" ForeColor="Red" runat="server"></asp:Label>
        </div>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="pnlApprove" runat="server" Visible="false">
    <div class="divContainer">
        <div class="divContainerTitle">
            Here is your shiny new avatar!
        </div>
        <div class="divContainerRow">
            <asp:Image ImageUrl="~/images/ProfileAvatar/ProfileImage.aspx" runat="server"/>
        </div>
        <div class="divContainerFooter">
            <asp:Button ID="btnStartNew" runat="server" Text="Don't like it?" OnClick="btnStartNew_Click" />
            <asp:Button ID="btnComplete" runat="server" Text="I like it!" OnClick="btnComplete_Click" />
        </div>
    </div>
</asp:Panel>
</asp:Content>