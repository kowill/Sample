using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Library;

namespace BigIntgereger.Tests
{
    [TestClass]
    public partial class BigIntgeregerTest
    {
        [TestMethod]
        public void Add()
        {
            Assert.AreEqual(new BigIntger(5) + new BigIntger(3), new BigIntger(8));
            Assert.AreEqual(new BigIntger(5) + new BigIntger(8), new BigIntger(13));
            Assert.AreEqual(new BigIntger(5) + new BigIntger(199), new BigIntger(204));
        }

        [TestMethod]
        public void Subtract()
        {
            Assert.AreEqual(new BigIntger(5) - new BigIntger(3), new BigIntger(2));
            Assert.AreEqual(new BigIntger(15) - new BigIntger(8), new BigIntger(7));
            Assert.AreEqual(new BigIntger(100) - new BigIntger(3), new BigIntger(97));
            Assert.AreEqual(new BigIntger(100) - new BigIntger(99), new BigIntger(1));
        }

        [TestMethod]
        public void multiplication()
        {
            Assert.AreEqual(3 * new BigIntger(3), new BigIntger(9));
            Assert.AreEqual(15 * new BigIntger(2), new BigIntger(30));
        }
    }
}
