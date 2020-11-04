using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenTea.Service.Test
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void Test1()
        {
            var r = new Request("/blah1/blah2/somefile.gif");
            Assert.AreEqual("somefile.gif", r.GetFileName());
            Assert.IsTrue(r.IsFile());
        }

        [TestMethod]
        public void Test2()
        {
            var r = new Request();
            Assert.AreEqual(string.Empty, r.GetFileName());
            Assert.IsFalse(r.IsFile());
        }

        [TestMethod]
        public void Test3()
        {
            var r = new Request("/something/something/page");
            Assert.AreEqual(string.Empty, r.GetFileName());
            Assert.IsFalse(r.IsFile());
        }
    }
}
