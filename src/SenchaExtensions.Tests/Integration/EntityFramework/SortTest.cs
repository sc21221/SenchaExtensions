﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenchaExtensions.Tests.Integration.EntityFramework
{
    [TestClass]
    public class SortTest : BaseTest
    {
        private ApplicationDbContext db;

        private SortConverter converter;

        [TestInitialize]
        public void Init()
        {
            base.Init(out db);

            converter = new SortConverter();
        }

        #region ASC
        [TestMethod]
        public void TrySort_Dir_Asc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"ASC\"}]";

            Sort sort = converter.ConvertFrom(request) as Sort;

            var result = db.Users
                .SortBy(sort)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "ABute");
        }
        #endregion ASC

        #region DESC
        [TestMethod]
        public void TrySort_Dir_Desc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"DESC\"}]";

            Sort sort = converter.ConvertFrom(request) as Sort;

            var result = db.Users
                .SortBy(sort)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "DZubak");
        }
        #endregion DESC
    }
}
