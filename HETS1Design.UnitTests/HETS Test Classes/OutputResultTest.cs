using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HETS1Design
{
    [TestClass]
    public class OutputResultTest
    {
        OutputResult o1;

        [TestInitialize]
        public void Initialize()
        {
            this.o1 = new OutputResult("something");
        }

        [TestMethod]
        public void Match_True()
        {
            o1.Match();
            Assert.IsTrue(o1.DidItMatch);
        }

        [TestMethod]
        public void Mismatch_NotTrue()
        {
            o1.Mismatch();
            Assert.IsFalse(o1.DidItMatch);
        }

    }
}
