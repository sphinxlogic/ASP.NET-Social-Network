using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Hosting;
using NUnit.Framework;
using System.Web;
using StructureMap;
using Fisharoo.FisharooCore.Core.Impl;

namespace Fisharoo.FisharooCoreTests.Core.Impl
{
    [TestFixture]
    public class CacheTester
    {
        [SetUp]
        public void Setup()
        {
            TestUtil.SetUpHttpContext();
        }



        [Test]
        public void ShouldCacheAnObject()
        {
            int initialValue = 123;
            Cache.Set("ShouldCache",initialValue);
            Assert.IsTrue(Cache.Exists("ShouldCache"));

            int cachedValue = (int) Cache.Get("ShouldCache");

            Assert.AreEqual(initialValue, cachedValue);

        }


        [Test]
        public void CacheExpiration()
        {
            int initialValue = 12345;
            //expire after 100 milliseconds
            Cache.Set("ThisCacheShouldExpire", initialValue, new TimeSpan(0,0,0,0, 100)); //100 milliseconds
            Cache.Set("ThisCacheShouldNOTExpire", initialValue, new TimeSpan(1, 0, 0, 0)); //1 day expiration

            Thread.Sleep(200);

            Assert.IsFalse(Cache.Exists("ThisCacheShouldExpire"));
            Assert.IsTrue(Cache.Exists("ThisCacheShouldNOTExpire"));

            int cachedValue = (int)Cache.Get("ThisCacheShouldNOTExpire");

            Assert.AreEqual(initialValue, cachedValue);

        }

        [Test]
        public void CacheFlush()
        {
            Cache.Set("Value1","Value1");
            Cache.Set("Value2", "Value2");
            Cache.Set("Value3", "Value3");
            
            Assert.IsTrue(Cache.Exists("Value1"));
            Assert.IsTrue(Cache.Exists("Value2"));
            Assert.IsTrue(Cache.Exists("Value3"));

            Cache.Flush();

            Assert.IsFalse(Cache.Exists("Value1"));
            Assert.IsFalse(Cache.Exists("Value2"));
            Assert.IsFalse(Cache.Exists("Value3"));
        }

        [TearDown]
        public void TearDown()
        {
            TestUtil.ClearHttpContext();
        }

    }
}