using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

public partial class JpegImage : System.Web.UI.Page
{
    private Random random = new Random();
    private IWebContext _webContext;

	private void Page_Load(object sender, System.EventArgs e)
	{
	    _webContext = ObjectFactory.GetInstance<IWebContext>();
	    _webContext.CaptchaImageText = GenerateRandomCode();

	    ICaptcha ci = ObjectFactory.GetInstance<ICaptcha>();
	    ci.Text = _webContext.CaptchaImageText;
	    ci.Width = 200;
	    ci.Height = 50;
	    ci.FamilyName = "Century Schoobook";

        Response.Clear();
		Response.ContentType = "image/jpeg";
		ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);
		ci.Dispose();
	}

    private string GenerateRandomCode()
	{
		string s = "";
		for (int i = 0; i < 6; i++)
			s = String.Concat(s, this.random.Next(10).ToString());
		return s;
	}

	override protected void OnInit(EventArgs e)
	{
		InitializeComponent();
		base.OnInit(e);
	}

	private void InitializeComponent()
	{    
		this.Load += new System.EventHandler(this.Page_Load);
	}
}
