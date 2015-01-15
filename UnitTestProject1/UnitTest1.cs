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
            orc.Position = new Position { X = 1, Y = 1 };

            Assert.AreEqual(orc.AttackPt, 2);
            Assert.AreEqual(orc.DefensePt, 1);
            Assert.AreEqual(orc.MovePt, 1);
            orc.Move(1,2);
            Assert.AreEqual(orc.MovePt, 0);
        }

        [TestMethod]
        public unsafe void TestCreationGameBoard()
        {
            WrapperAlgo w = new WrapperAlgo();
            var res = w.createGameBoard(6);
            int * p = res;
           

            //TODOSW
        }

        [TestMethod]
        public unsafe void TestAttack()
        {
            DwarfUnit d = new DwarfUnit();
            ElfUnit e = new ElfUnit();

            e.Attack(d, 1);

            Assert.AreEqual(4, d.HitPt);
        }
    }
}
