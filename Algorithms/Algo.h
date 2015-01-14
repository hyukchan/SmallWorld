#pragma once

#define DLL _declspec(dllexport)
#define EXTERNC extern "C"

class DLL Algo {

public:
	static enum Tile { MOUNTAIN, DESERT, PLAIN, FOREST };
	static struct Position {
		int x;
		int y;
	};

	Algo* Algo_new();
	void delete_Algo(Algo* algo);
	int* createGameBoard(int n);
	int* startingPositions(int* map, int size);
	int* mapCreation(int size);
	void freeMap(int* map);
	int* possibleMoves(double movePt, int* moves, int x, int y, int size, int* map, int peuple);


};