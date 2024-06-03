using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;
using File=Fisharoo.FisharooCore.Core.Domain.File;

public partial class Files_ReceiveFiles : System.Web.UI.Page
{
    //public string ImageFolder = "";
    
    //Dictionary<string,int> sizesToMake = new Dictionary<string,int>();
    //private int sizeTiny = 50;
    //private int sizeSmall = 200;
    //private int sizeMedium = 500;
    //private int sizeLarge = 1000;

    //private IUserSession _userSession;
    //private IWebContext _webContext;
    //private IFileRepository _fileRepository;
    //private IAccountRepository _accountRepository;

    //int NewWidth = 0;
    //int NewHeight = 0;

    //string saveToFolder = "files";
    private IFileService _fileService;
    private IWebContext _webContext;
    protected void Page_Load(object sender, System.EventArgs e) 
    {
        _fileService = ObjectFactory.GetInstance<IFileService>();
        _webContext = ObjectFactory.GetInstance<IWebContext>();

        _fileService.UploadPhotos(_webContext.FileTypeID, _webContext.AccountID, _webContext.Files, _webContext.AlbumID);

        //_userSession = ObjectFactory.GetInstance<IUserSession>();
        //_webContext = ObjectFactory.GetInstance<IWebContext>();
        //_fileRepository = ObjectFactory.GetInstance<IFileRepository>();
        //_accountRepository = ObjectFactory.GetInstance<IAccountRepository>();

        //sizesToMake.Add("T",sizeTiny);
        //sizesToMake.Add("S",sizeSmall);
        //sizesToMake.Add("M",sizeMedium);
        //sizesToMake.Add("L",sizeLarge);

        ////determine save to folder
        //switch (_webContext.FileTypeID)
        //{
        //    case 1:
        //        saveToFolder = "Photos/";
        //        break;

        //    case 2:
        //        saveToFolder = "Videos/";
        //        break;

        //    case 3:
        //        saveToFolder = "Audios/";
        //        break;
        //}

        ////make sure the directory is ready for use
        //saveToFolder += DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "/";
        //if (!Directory.Exists(Server.MapPath(saveToFolder)))
        //    Directory.CreateDirectory(Server.MapPath(saveToFolder));

        //Account account = _accountRepository.GetAccountByID(_webContext.AccountID);

        //HttpFileCollection uploadedFiles =  Request.Files;
        //string Path = Server.MapPath(saveToFolder);
        //for(int i = 0 ; i < uploadedFiles.Count ; i++)
        //{
        //    HttpPostedFile F = uploadedFiles[i];
        //    if(uploadedFiles[i] != null && F.ContentLength > 0)
        //    {   
        //        string folderID = _webContext.AlbumID.ToString();
        //        string fileType = "1";
        //        string uploadedFileName = F.FileName.Substring(F.FileName.LastIndexOf("\\") + 1);
        //        string extension = uploadedFileName.Substring(uploadedFileName.LastIndexOf(".") + 1);
        //        Guid guidName = Guid.NewGuid();
        //        string fullFileName = Path + "/" + guidName.ToString() + "__O." + extension;
        //        bool goodFile = true;

        //        //create the file
        //        File file = new File();

        //        #region "Determine file type"
        //        switch (fileType)
        //        {
        //            case "1":
        //                file.FileSystemFolderID = (int)FileSystemFolder.Paths.Pictures;
        //                switch (extension.ToLower())
        //                {
        //                    case "jpg":
        //                        file.FileTypeID = (int)File.Types.JPG;
        //                        break;
        //                    case "gif":
        //                        file.FileTypeID = (int)File.Types.GIF;
        //                        break;
        //                    case "jpeg":
        //                        file.FileTypeID = (int)File.Types.JPEG;
        //                        break;
        //                    default:
        //                        goodFile = false;
        //                        break;
        //                }
        //                break;

        //            case "2":
        //                file.FileSystemFolderID = (int)FileSystemFolder.Paths.Videos;
        //                switch (extension.ToLower())
        //                {
        //                    case "wmv":
        //                        file.FileTypeID = (int)File.Types.WMV;
        //                        break;
        //                    case "flv":
        //                        file.FileTypeID = (int)File.Types.FLV;
        //                        break;
        //                    case "swf":
        //                        file.FileTypeID = (int)File.Types.SWF;
        //                        break;
        //                    default:
        //                        goodFile = false;
        //                        break;
        //                }
        //                break;

        //            case "3":
        //                file.FileSystemFolderID = (int)FileSystemFolder.Paths.Audios;
        //                switch (extension.ToLower())
        //                {
        //                    case "wav":
        //                        file.FileTypeID = (int)File.Types.WAV;
        //                        break;
        //                    case "mp3":
        //                        file.FileTypeID = (int)File.Types.MP3;
        //                        break;
        //                    case "flv":
        //                        file.FileTypeID = (int)File.Types.FLV;
        //                        break;
        //                    default:
        //                        goodFile = false;
        //                        break;
        //                }
        //                break;
        //        }
        //        #endregion

        //        file.Size = F.ContentLength;
        //        file.AccountID = account.AccountID;
        //        file.DefaultFolderID = Convert.ToInt32(folderID);
        //        file.FileName = uploadedFileName;
        //        file.FileSystemName = guidName;
        //        file.Description = "";
        //        file.IsPublicResource = false;

        //        if (goodFile)
        //        {
        //            _fileRepository.SaveFile(file);

        //            F.SaveAs(fullFileName);

        //            if(Convert.ToInt32(fileType) == ((int)Folder.Types.Picture))
        //            {
        //                Resize(F,saveToFolder,guidName,extension);
        //            }
        //        }
        //    }
        //}
    }

    //public void Resize(HttpPostedFile F, string SaveToFolder, Guid SystemFileNamePrefix, string Extension)
    //{
    //    //Makes all the different sizes in the sizesToMake collection
    //    foreach (KeyValuePair<string, int> pair in sizesToMake)
    //    {
    //        using(System.Drawing.Image image = System.Drawing.Image.FromStream(F.InputStream))
    //        //determine the thumbnail sizes
    //        using(Bitmap bitmap = new Bitmap(image))
    //        {
    //            decimal Ratio;
    //            if(bitmap.Width > bitmap.Height)
    //            {
    //                Ratio = (decimal) pair.Value / bitmap.Width;
    //                NewWidth = pair.Value;
    //                decimal Temp = bitmap.Height * Ratio;
    //                NewHeight = (int)Temp;
    //            }
    //            else
    //            {
    //                Ratio = (decimal) pair.Value / bitmap.Height;
    //                NewHeight = pair.Value;
    //                decimal Temp = bitmap.Width * Ratio;
    //                NewWidth = (int)Temp;
    //            }
    //        }

    //        using(System.Drawing.Image image = System.Drawing.Image.FromStream(F.InputStream))
    //        using(Bitmap bitmap = new Bitmap(image, NewWidth, NewHeight))
    //        {
    //            bitmap.Save(Server.MapPath(SaveToFolder + "/" + SystemFileNamePrefix.ToString() + "__" + pair.Key + "." + Extension), image.RawFormat);
    //        }
    //    }
    //}
}
