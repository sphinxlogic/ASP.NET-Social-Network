using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace Fisharoo.FisharooCoreTests.Core.DataAccess.Impl
{
    [TestFixture]
    public class AccountRepositoryTester
    {
        private MockRepository _mocks;
        private IAccountRepository _accountRepository;
        private Connection conn = null;

        [SetUp]
        public void SetUp()
        {
            conn = new Connection();
            _mocks = new MockRepository();
            _accountRepository = _mocks.Stub<IAccountRepository>();
        }

        [Test]
        public void ShouldRetrieveByID()
        {
            Account a = new Account();
            a.FirstName = "Andrew";
            a.LastName = "Siemer";
            a.Email = "asiemer@hotmail.com";
            a.AccountID = 1;

            Expect.Call(_accountRepository.GetAccountByID(1)).Return(a);
            _mocks.ReplayAll();

            Account returnAccount = _accountRepository.GetAccountByID(1);
            
            Assert.AreEqual(returnAccount,a);
        }

        [Test]
        public void ShouldSaveAnAccount()
        {
            using (FisharooDataContext fdc = conn.GetContext())
            {
                Account a = new Account();
                a.FirstName = "AccountToDelete";
                a.LastName = "test object";
                a.Email = "asiemer@hotmail.com";

                fdc.Accounts.InsertOnSubmit(a);
                fdc.SubmitChanges();

                var accounts = from dba in fdc.Accounts
                    where dba.Email == a.Email
                    select dba;

                foreach (Account account in accounts)
                {
                    Assert.AreEqual(account.Email, a.Email);
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            
        }
    }
}
