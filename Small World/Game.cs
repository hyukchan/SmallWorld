﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{

    public interface GameInterface
    {
        Player winner();

        void changePlayer();

        List<Unit> selectUnit(int x, int y);

        List<Unit> selectOpponentUnit(int x, int y);

        bool canMove(Unit unit, int x, int y);

        int askToMove(Unit unit, int x, int y);

        void checkEndOfGame();

        bool save();

        bool saveAs(string filename);

        Game load(string filename);

        //TODOSW restaurer ?
    }

    public unsafe class Game : GameInterface
    {

        private Player FirstPlayer;

        private Player currentPlayer;

        public Game()
        {
            throw new System.NotImplementedException();
        }
    
        public GameBoard GameBoard
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Player Player1
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {

            }
        }

        public Player Player2
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void SelectUnit()
        {
            throw new System.NotImplementedException();
        }

        public void EndTurn()
        {
            throw new System.NotImplementedException();
        }

        public void MoveUnit()
        {
            throw new System.NotImplementedException();
        }
    }
}
