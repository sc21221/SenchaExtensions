using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SenchaExtensions.Tests.Integration.EntityFramework
{
    [TestClass]
    public class PagingTest : BaseTest
    {
        private ApplicationDbContext db;

        [TestInitialize]
        public void Init()
        {
            base.Init(out db);
        }

        [TestMethod]
        public void TryGetPagingResult()
        {
            var result = db
                .Users
                .GetPagingResult(1, 0, 100, SortExtensions.Create("Id"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success == true);
            Assert.IsTrue(result.Total == 1002);
            Assert.IsTrue(result.Items.Count() == 100);
        }
    }
}
