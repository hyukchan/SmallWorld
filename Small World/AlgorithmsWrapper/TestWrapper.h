#pragma once

#include "../../Algorithms/Algo.h"
#pragma comment(lib, "../Debug/Algorithms.lib")
using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo{
	private :
		Algo* algo;

	public :
		WrapperAlgo(){ algo = new Algo(); }
		~WrapperAlgo(){ delete(algo); }
		int createGameBoard(){ return algo->createGameBoard(6); }
	};
}