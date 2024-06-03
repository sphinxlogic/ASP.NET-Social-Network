using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class AccountRepository : IAccountRepository
    {
        private Connection conn;

        public AccountRepository()
        {
            conn = new Connection();
        }

        public Account GetAccountByID(int AccountID)
        {
            Account account = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                account = (from a in dc.Accounts
                           where a.AccountID == AccountID
                           select a).First();
            }
            return account;
        }

        public Account GetAccountByEmail(string Email)
        {
            Account account = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                account = (from a in dc.Accounts
                    where a.Email == Email
                    select a).First();
            }
            return account;
        }

        public Account GetAccountByUsername(string Username)
        {
            Account account = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                account = (from a in dc.Accounts
                    where a.Username == Username
                    select a).First();
            }

            return account;
        }

        public void AddPermission(Account account, Permission permission)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                AccountPermission ap = new AccountPermission();
                ap.AccountID = account.AccountID;
                ap.PermissionID = permission.PermissionID;
                dc.AccountPermissions.InsertOnSubmit(ap);
                dc.SubmitChanges();
            }
        }

        public void SaveAccount(Account account)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(account.AccountID > 0)
                {
                    dc.Accounts.Attach(account, true);
                }
                else
                {
                    dc.Accounts.InsertOnSubmit(account);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteAccount(Account account)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                dc.Accounts.DeleteOnSubmit(account);
                dc.SubmitChanges();
            }
        }

        public List<Account> GetAllAccounts(Int32 PageNumber)
        {
            IEnumerable<Account> accounts = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                 accounts = (from a in dc.Accounts
                                orderby a.Username
                               select a).Skip((PageNumber - 1) * 10).Take(10);
            }

            return accounts.ToList();
        }
    }
}
