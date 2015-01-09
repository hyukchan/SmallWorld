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
	double* costTab(int size);
	void freeCost(double* costTab);
	void initializeOrcMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt);
	void orcPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void orcPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void orcMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);
	void initializeDwarfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt);
	void dwarfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void dwarfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void dwarfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);
	void initializeElfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt);
	void elfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void elfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void elfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);

	void orcPossibleMovement1(int* map, int size, int x, int y, int* moves, double* cost);

};