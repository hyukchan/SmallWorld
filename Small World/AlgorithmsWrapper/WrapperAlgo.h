#ifndef __WRAPPER__
#define __WRAPPER__

#include "../../Algorithms/Algo.h"

using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo{
	private:
		Algo* algo;

	public:
		WrapperAlgo(){ algo = new Algo(); }
		~WrapperAlgo(){ delete(algo); }
		int * createGameBoard(int size){ return algo->createGameBoard(size); }
		int * startingPositions(int * map, int size){ return algo->startingPositions(map, size); }
		int * mapCreation(int size){ return algo->mapCreation(size); }
		void freeMap(int * map){ algo->freeMap(map); }
		int* possibleMoves(double movePt, int* moves, int x, int y, int size, int* map, int peuple){ return algo->possibleMoves(movePt, moves, x, y,size,map,peuple); }
		
	};
}

#endif