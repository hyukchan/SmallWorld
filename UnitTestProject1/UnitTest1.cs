using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Small_World;
using Wrapper;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreationUnit()
        {
            Unit u = new OrcUnit();
            u.Position = new Position { X = 1, Y = 1 };
            Assert.AreEqual(u.AttackPt, 2);
            Assert.AreEqual(u.DefensePt, 1);
            Assert.AreEqual(u.MovePt, 1);

            Unit v = new ElfUnit();
            u.Position = new Position { X = 1, Y = 1 };
            Assert.AreEqual(v.AttackPt, 2);
            Assert.AreEqual(v.DefensePt, 1);
            Assert.AreEqual(v.MovePt, 1);

            Unit w = new DwarfUnit();
            u.Position = new Position { X = 1, Y = 1 };
            Assert.AreEqual(w.AttackPt, 2);
            Assert.AreEqual(w.DefensePt, 1);
            Assert.AreEqual(w.MovePt, 1);
        }

        [TestMethod]
        public unsafe void TestCreationGameBoard()
        {
            WrapperAlgo w = new WrapperAlgo();
            var res = w.createGameBoard(6);
            int* p = res;


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

        [TestMethod]
        public unsafe void TestGameBoard()
        {
            BuilderGameBoard b = new BuilderDemoGameBoard();
            Assert.AreEqual(6, b.Size);
            BuilderGameBoard c = new BuilderMediumGameBoard();
            Assert.AreEqual(10, c.Size);
            BuilderGameBoard d = new BuilderLargeGameBoard();
            Assert.AreEqual(14, d.Size);
        }

        [TestMethod]
        public void TestBuilderGameBoard()
        {
            BuilderGameBoard b = new BuilderDemoGameBoard();
            List<Tile> l1 = new List<Tile>();
            l1 = b.build();
            Assert.AreEqual(36, l1.Count);

            BuilderGameBoard c = new BuilderMediumGameBoard();
            List<Tile> l2 = new List<Tile>();
            l2 = c.build();
            Assert.AreEqual(100, l2.Count);

            BuilderGameBoard d = new BuilderLargeGameBoard();
            List<Tile> l3 = new List<Tile>();
            l3 = d.build();
            Assert.AreEqual(196, l3.Count);

        }

        [TestMethod]
        public void TestTileFactory()
        {
            TileFactory t = new TileFactory();
            Assert.AreEqual(new Desert().GetType(), t.GetDesert().GetType());
            Assert.AreEqual(new Mountain().GetType(), t.GetMountain().GetType());
            Assert.AreEqual(new Plain().GetType(), t.GetPlain().GetType());
            Assert.AreEqual(new Forest().GetType(), t.GetForest().GetType());
        }

        [TestMethod]
        public void TestPeopleFactory()
        {
            PeopleFactory p = new PeopleFactory();
            Assert.AreEqual(new Orc().GetType(), p.CreatePeople(PeopleFactory.ORC).GetType());
            Assert.AreEqual(new Elf().GetType(), p.CreatePeople(PeopleFactory.ELF).GetType());
            Assert.AreEqual(new Dwarf().GetType(), p.CreatePeople(PeopleFactory.DWARF).GetType());
        }

        [TestMethod]
        public unsafe void TestUnitTurn()
        {
            WrapperAlgo w = new WrapperAlgo();
            Unit u = new OrcUnit();
            u.endTurn();
            Assert.AreEqual(true, u.TurnEnded);
            u.MovePt = 0;
            u.SizeMap = 6;
            u.TabMap = w.createGameBoard(u.SizeMap);
            u.Moves = w.mapCreation(u.SizeMap);
            u.newTurn();
            Assert.AreEqual(Unit.MOVE_PT, u.MovePt);
            Assert.AreEqual(false, u.TurnEnded);
        }

        [TestMethod]
        public unsafe void TestUpdateGamePointsElf()
        {
            //test Elf
            WrapperAlgo w = new WrapperAlgo();
            Unit u = new ElfUnit();
            u.MovePt = 0;
            u.SizeMap = 6;
            u.TabMap = w.createGameBoard(u.SizeMap);
            u.Moves = w.mapCreation(u.SizeMap);
            u.Position = new Position { X = 0, Y = 0 };
            u.TabMap[0] = Tile.PLAIN;
            u.UpdateGamePoints();
            Assert.AreEqual(1, u.GamePt);
            u.TabMap[0] = Tile.MOUNTAIN;
            u.UpdateGamePoints();
            Assert.AreEqual(1, u.GamePt);
            u.TabMap[0] = Tile.DESERT;
            u.UpdateGamePoints();
            Assert.AreEqual(1, u.GamePt);
            u.TabMap[0] = Tile.FOREST;
            u.UpdateGamePoints();
            Assert.AreEqual(1, u.GamePt);
        }

        [TestMethod]
        public unsafe void TestUpdateGamePointsDwarf()
        {
            //test nain
            WrapperAlgo w = new WrapperAlgo();
            Unit v = new DwarfUnit();
            v.MovePt = 0;
            v.SizeMap = 6;
            v.TabMap = w.createGameBoard(v.SizeMap);
            v.Moves = w.mapCreation(v.SizeMap);
            v.Position = new Position { X = 0, Y = 0 };
            v.TabMap[0] = Tile.PLAIN;
            v.UpdateGamePoints();
            Assert.AreEqual(0, v.GamePt);
            v.TabMap[0] = Tile.MOUNTAIN;
            v.UpdateGamePoints();
            Assert.AreEqual(1, v.GamePt);
            v.TabMap[0] = Tile.DESERT;
            v.UpdateGamePoints();
            Assert.AreEqual(1, v.GamePt);
            v.TabMap[0] = Tile.FOREST;
            v.UpdateGamePoints();
            Assert.AreEqual(1, v.GamePt);
        }

        [TestMethod]
        public unsafe void TestUpdateGamePointsOrc()
        {
            //test Orc
            WrapperAlgo w = new WrapperAlgo();
            Unit x = new OrcUnit();
            x.MovePt = 0;
            x.SizeMap = 6;
            x.TabMap = w.createGameBoard(x.SizeMap);
            x.Moves = w.mapCreation(x.SizeMap);
            x.Position = new Position { X = 0, Y = 0 };
            x.TabMap[0] = Tile.PLAIN;
            x.UpdateGamePoints();
            Assert.AreEqual(0, x.GamePt);
            ((OrcUnit)x).BonusPt = 1;
            x.UpdateGamePoints();
            Assert.AreEqual(1, x.GamePt);
            x.TabMap[0] = Tile.MOUNTAIN;
            x.UpdateGamePoints();
            Assert.AreEqual(2, x.GamePt);
            x.TabMap[0] = Tile.DESERT;
            x.UpdateGamePoints();
            Assert.AreEqual(2, x.GamePt);
            x.TabMap[0] = Tile.FOREST;
            x.UpdateGamePoints();
            Assert.AreEqual(2, x.GamePt);
        }

    }





}

