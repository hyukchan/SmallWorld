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
	Position* getStartPositions(int* gameBoard);
	Position* getBestMoves(int* gameBoard);

};