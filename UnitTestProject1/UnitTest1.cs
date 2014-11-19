using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreationOrcUnit()
        {
            OrcUnit orc = new OrcUnit();

            Assert.AreEqual(orc.AttackPt, 2);
            Assert.AreEqual(orc.DefensePt, 1);
            Assert.AreEqual(orc.MovePt, 1);
            orc.Move(new Position(1,2));
            Assert.AreEqual(orc.MovePt, 0);
        }
    }
}
