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
		array<Tile^>^ createGameBoard(int nb, TileFactory^ factory){
			array<Tile^>^ res = gcnew array<Tile^>(nb*nb);
			int* nativeRes = algo->createGameBoard(nb);

			for (int i = 0; i < nb*nb; i++) {
				res[i] = factory-> nativeRes[i];
			}

			return res;
		}
	};
}