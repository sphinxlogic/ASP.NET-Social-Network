//http://www.c-sharpcorner.com/UploadFile/dsandor/ActiveXInNet11102005040748AM/ActiveXInNet.aspx

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace FisharooOutlookImporter
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public partial class OutlookImporter : UserControl
    {
        private Outlook._Application outlookObj;
        public OutlookImporter()
        {
            InitializeComponent();
            outlookObj = new Outlook.Application();
        }

        private void GetAddresses()
        {
            Outlook.MAPIFolder fldContacts =
                (Outlook.MAPIFolder)outlookObj.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);

            clbAddresses.Items.Clear();

            foreach (object item in fldContacts.Items)
            {
                Outlook.ContactItem contact = item as Outlook.ContactItem;
                if (contact != null)
                {
                    if (!string.IsNullOrEmpty(contact.Email1Address) && !contact.Email1Address.Contains("/"))
                        clbAddresses.Items.Add(contact.Email1Address + " (" + contact.FirstName + " " + contact.LastName + ")");
                    if (!string.IsNullOrEmpty(contact.Email2Address) && !contact.Email2Address.Contains("/"))
                        clbAddresses.Items.Add(contact.Email2Address + " (" + contact.FirstName + " " + contact.LastName + ")");
                    if (!string.IsNullOrEmpty(contact.Email3Address) && !contact.Email3Address.Contains("/"))
                        clbAddresses.Items.Add(contact.Email3Address + " (" + contact.FirstName + " " + contact.LastName + ")");
                }
            }
        }

        private void btnGetAddresses_Click(object sender, EventArgs e)
        {
            GetAddresses();
        }
    }
}