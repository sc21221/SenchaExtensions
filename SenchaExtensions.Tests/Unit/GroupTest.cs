using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SenchaExtensions.Tests.Unit
{
    [TestClass]
    public class GroupTest
    {
        private GroupConverter converter;

        [TestInitialize]
        public void Init()
        {
            converter = new GroupConverter();
        }

        #region ASC
        [TestMethod]
        public void TryGroup_Dir_Asc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"ASC\"}]";

            Group group = converter.ConvertFrom(request) as Group;

            var result = MockData
                .Users()
                .AsQueryable()
                .GroupBy(group)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "ABute");
        }
        #endregion ASC

        #region DESC
        [TestMethod]
        public void TryGroup_Dir_Desc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"DESC\"}]";

            Group group = converter.ConvertFrom(request) as Group;

            var result = MockData
                .Users()
                .AsQueryable()
                .GroupBy(group)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "DZubak");
        }
        #endregion DESC
    }
}
