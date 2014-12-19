#pragma once

class Algo {

public:
	static enum Tile { MOUNTAIN, DESERT, PLAIN, FOREST };
	static struct Position {
		int x;
		int y;
	};

	Algo* Algo_new();
	void delete_Algo(Algo* algo);
	int* createGameBoard(int n);
	int* startingPositions(int* map, int n);
	int* mapCreation(int size);
	void freeMap(int* map);
	double* CostTab(int size);
	void freeCost(double* costTab);
	void initializeOrcMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt);
	void orcPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void orcPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void orcMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);
	void dwarfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void dwarfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void dwarfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);
	void elfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost);
	void elfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost);
	void elfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves);

};