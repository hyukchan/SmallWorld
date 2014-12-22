#ifndef _WRAPPER
#define _WRAPPER
#pragma once

#include "../../Algorithms/Algo.h"

#pragma comment(lib, "../Debug/Algorithms.lib")
using namespace System;
using namespace Small_World;

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
		double * costTab(int size){ return algo->costTab(size); }
		void freeCost(double* costTab){ algo->freeCost(costTab); }
		void initializeOrcMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt){ algo->initializeOrcMvt(map, size, x, y, cost, moves, movPt); }
		void initializeDwarfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt){ algo->initializeDwarfMvt(map, size, x, y, cost, moves, movPt); }
		void initializeElfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt){ algo->initializeElfMvt(map, size, x, y, cost, moves, movPt); }
	};
}

#endif