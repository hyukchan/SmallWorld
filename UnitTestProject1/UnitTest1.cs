using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;
using Wrapper;

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
            orc.Move(new Position { X = 1, Y = 2});
            Assert.AreEqual(orc.MovePt, 0);
        }

        [TestMethod]
        public void TestCreationGameBoard()
        {
            WrapperAlgo w = new WrapperAlgo();
            //var res = w.createGameBoard(6);
            //Assert.AreEqual(42, res);
            //TODOSW
        }
    }
}
