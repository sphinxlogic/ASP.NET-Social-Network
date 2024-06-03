using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    string saveToFolder = "files";
    protected void Page_Load(object sender, System.EventArgs e) {
      HttpFileCollection uploadedFiles =  Request.Files;
      string Path = Server.MapPath(saveToFolder);
      for(int i = 0 ; i < uploadedFiles.Count ; i++)
      {
        HttpPostedFile F = uploadedFiles[i];
        if(uploadedFiles[i] != null && F.ContentLength > 0)
        {   
          string newName = F.FileName.Substring(F.FileName.LastIndexOf("\\") + 1);
          F.SaveAs(Path + "/" + newName);
         }
       }
    }
}
