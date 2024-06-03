using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Tags.Interface;
using Fisharoo.FisharooWeb.Tags.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Tags
{
    public partial class Tags : System.Web.UI.Page, ITags
    {
        private IWebContext _webContext;
        private TagsPresenter _tagsPresenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _tagsPresenter = new TagsPresenter();

            _tagsPresenter.Init(this, IsPostBack);
        }

        public void LoadUI(List<SystemObjectTagWithObjects> tagWithObjects)
        {
            repAccounts.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 1);
            repAccounts.DataBind();
            repProfiles.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 2);
            repProfiles.DataBind();
            repBlogs.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 3);
            repBlogs.DataBind();
            repPosts.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 4);
            repPosts.DataBind();
            repFiles.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 5);
            repFiles.DataBind();
            repGroups.DataSource = tagWithObjects.Where(t => t.SystemObjectTag.SystemObjectID == 6);
            repGroups.DataBind();

            if (repGroups.Items.Count == 0)
                repGroups.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));

            if (repFiles.Items.Count == 0)
                repFiles.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));

            if (repPosts.Items.Count == 0)
                repPosts.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));

            if (repBlogs.Items.Count == 0)
                repBlogs.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));

            if (repProfiles.Items.Count == 0)
                repProfiles.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));

            if(repAccounts.Items.Count == 0)
                repAccounts.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">No tagged items</td></tr>"));
        }

        public void SetTitle(string TagName)
        {
            ((SiteMaster)Master).Title = TagName;
        }
    }
}
