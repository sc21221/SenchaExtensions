using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SenchaExtensions.Tests.Integration.EntityFramework
{
    [TestClass]
    public class GroupTest : BaseTest
    {
        private ApplicationDbContext db;
        private GroupConverter converter;

        [TestInitialize]
        public void Init()
        {
            base.Init(out db);
            converter = new GroupConverter();
        }

        #region ASC
        [TestMethod]
        public void TryGroup_Dir_Asc()
        {
            object request = "[{\"property\":\"dateCreated\",\"direction\":\"ASC\"}]";

            Group group = converter.ConvertFrom(request) as Group;

            var result = db
                .Users
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

            var result = db
                .Users
                .GroupBy(group)
                .ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.First().Login == "DZubak");
        }
        #endregion DESC
    }
}
