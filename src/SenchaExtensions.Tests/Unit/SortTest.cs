using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SenchaExtensions.Tests.Unit
{
    [TestClass]
    public class SortTest
    {
        private SortConverter converter;

        [TestInitialize]
        public void Init()
        {
            converter = new SortConverter();
        }

        #region ASC
        [TestMethod]
        public void TrySort_Dir_Asc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"ASC\"}]";

            Sort sort = converter.ConvertFrom(request) as Sort;

            var result = MockData
                .Users()
                .AsQueryable()
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

            var result = MockData
                .Users()
                .AsQueryable()
                .SortBy(sort)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "DZubak");
        }
        #endregion DESC
    }
}
