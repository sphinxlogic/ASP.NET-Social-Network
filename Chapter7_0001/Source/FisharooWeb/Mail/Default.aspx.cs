using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Mail.Interface;
using Fisharoo.FisharooWeb.Mail.Presenter;

namespace Fisharoo.FisharooWeb.Mail
{
    public partial class Default : System.Web.UI.Page, IDefault 
    {
        private DefaultPresenter _presenter;
        protected override void OnInit(EventArgs e)
        {
            _presenter = new DefaultPresenter();
            _presenter.Init(this);
            base.OnInit(e);
        }

        public void LoadMessages(List<MessageWithRecipient> Messages)
        {
            repMessages.DataSource = Messages;
            repMessages.DataBind();
        }

        protected void repMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MessageWithRecipient wmr = (MessageWithRecipient) e.Item.DataItem;
                HyperLink linkMessage = e.Item.FindControl("linkMessage") as HyperLink;
                HyperLink linkProfile = e.Item.FindControl("linkProfile") as HyperLink;
                CheckBox chkMessage = e.Item.FindControl("chkMessage") as CheckBox;

                chkMessage.Attributes.Add("MessageID",wmr.Message.MessageID.ToString());
                linkMessage.NavigateUrl = "~/mail/ReadMessage.aspx?MessageID=" + wmr.Message.MessageID.ToString();
                linkProfile.NavigateUrl = "~/" + wmr.Sender.Username;
            }
        }

        public List<Int32> ExtractSelectedMessages()
        {
            List<Int32> result = new List<Int32>();
            foreach (RepeaterItem item in repMessages.Items)
            {
                if(item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkMessage = item.FindControl("chkMessage") as CheckBox;
                    Int32 messageID = Convert.ToInt32(chkMessage.Attributes["MessageID"]);
                    if(chkMessage.Checked)
                        result.Add(messageID);
                }
            }
            return result;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.DeleteSelected();
        }

        public void DisplayPageNavigation(Int32 PageCount, MessageFolders folder, Int32 CurrentPage)
        {
            if(PageCount == CurrentPage)
                linkNext.Visible = false;
            if (CurrentPage == 1)
                linkPrevious.Visible = false;

            linkNext.NavigateUrl = "~/mail/default.aspx?folder=" + ((int) folder).ToString() + "&page=" +
                                   (CurrentPage + 1).ToString();
            linkPrevious.NavigateUrl = "~/mail/default.aspx?folder=" + ((int) folder).ToString() + "&page=" +
                                       (CurrentPage - 1).ToString();

            for(int i = 1; i<=PageCount;i++)
            {
                HyperLink link = new HyperLink();
                link.Text = i.ToString();
                link.NavigateUrl = "~/mail/default.aspx?folder=" + ((int)folder).ToString() + "&page=" + i.ToString();
                phPages.Controls.Add(link);
                phPages.Controls.Add(new LiteralControl("&nbsp;"));
            }
        }
    }
}
