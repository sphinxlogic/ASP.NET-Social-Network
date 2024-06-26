﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using StructureMap;
using Fisharoo.FisharooCore.Core.Domain;
using Account=Fisharoo.FisharooCore.Core.Domain.Account;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class AccountRepository : IAccountRepository
    {
        private Connection conn;
        private IAlertService _alertService;
        private IConfiguration _configuration;

        public AccountRepository()
        {
            conn = new Connection();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        //CHAPTER 10
        public List<Account> GetApprovedAccountsByGroupID(int GroupID, int PageNumber)
        {
            List<Account> result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Account> accounts = (from a in dc.Accounts
                                                 join m in dc.GroupMembers on a.AccountID equals m.AccountID
                                                 where m.GroupID == GroupID && m.IsApproved
                                                 select a).Skip((_configuration.NumberOfRecordsInPage*(PageNumber-1)))
                                                 .Take(_configuration.NumberOfRecordsInPage);
                result = accounts.ToList();
            }
            return result;
        }
        
        //CHAPTER 10
        public List<Account> GetAccountsToApproveByGroupID(int GroupID)
        {
            List<Account> result = null;
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Account> accounts = (from a in dc.Accounts
                                                 join m in dc.GroupMembers on a.AccountID equals m.AccountID
                                                 where m.GroupID == GroupID && !m.IsApproved
                                                 select a);
                result = accounts.ToList();
            }
            return result;
        }

        public Account GetAccountByID(int AccountID)
        {
            Account account = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                account = (from a in dc.Accounts
                           where a.AccountID == AccountID
                           select a).FirstOrDefault();
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
                    select a).FirstOrDefault();
            }
            return account;
        }

        //CHAPTER 5
        public List<Account> SearchAccounts(string SearchText)
        {
            List<Account> result = new List<Account>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Account> accounts = from a in dc.Accounts
                        where(a.FirstName + " " + a.LastName).Contains(SearchText) ||
                            a.Email.Contains(SearchText) ||
                            a.Username.Contains(SearchText)
                        select a;
                result = accounts.ToList();
            }
            return result;
        }

        public Account GetAccountByUsername(string Username)
        {
            Account account = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                try
                {
                    account = (from a in dc.Accounts
                               where a.Username == Username
                               select a).FirstOrDefault();
                }
                catch
                {
                    //oops
                }
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
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                account.LastUpdateDate = DateTime.Now;
                if(account.AccountID > 0)
                {
                    dc.Accounts.Attach(account, true);
                    _alertService.AddAccountModifiedAlert(account);
                }
                else
                {
                    account.CreateDate = DateTime.Now;
                    dc.Accounts.InsertOnSubmit(account);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteAccount(Account account)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                dc.Accounts.Attach(account, true);
                dc.Accounts.DeleteOnSubmit(account);
                dc.SubmitChanges();
            }
        }

        public List<Account> GetAllAccounts(Int32 PageNumber)
        {
            List<Account> result = new List<Account>();

            using (FisharooDataContext dc = conn.GetContext())
            {
                 IEnumerable<Account> accounts = (from a in dc.Accounts
                                orderby a.Username
                               select a).Skip((PageNumber - 1) * 10).Take(10);
                result = accounts.ToList();
            }

            return result;
        }
    }
}
