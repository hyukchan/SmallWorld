#include "Algo.h"
#include <iostream>
#include <stdio.h>
#include <stdlib.h> 
#include <time.h>
#include <algorithm>
using namespace std;

#define PLAIN 0
#define DESERT 1
#define FOREST 2
#define MOUNTAIN 3

#define INIT 0
#define IMPOSSIBLE 1
#define POSSIBLE 2

#define MVTPT 1

/**
* Constructeur
*/
Algo* Algo::Algo_new()
{
	return new Algo();
}
/**
* Destructeur
*/
void Algo::delete_Algo(Algo* algo){
	delete algo;
}


int* Algo::createGameBoard(int n){
	srand(time(NULL));

	int* tiles = new int[n*n];

	int initNbTiles = n*n / 4;

	int LeftTiles[] = { initNbTiles, initNbTiles, initNbTiles, initNbTiles};

	unsigned int currentTileType, i, j;

	for (i = 0; i < n; i++){
		for (j = 0; j < n; j++){
			currentTileType = rand() % 4;
			while (LeftTiles[currentTileType] == 0){
				currentTileType = rand() % 4;
			}
			tiles[i*n + j] = currentTileType;
			LeftTiles[currentTileType]--;
		}

	}

	return tiles;
}

int* Algo::startingPositions(int* map, int size){

	srand((unsigned int)time(NULL));

	int* coordinates = (int*)malloc(4 * sizeof(int));

	int i, j, i1, j1;
	int pos = rand() % 2;
	if (pos == 0)
	{
		i = 0;
		j = 0;
		i1 = size - 1;
		j1 = size - 1;
	}
	else
	{
		i = size - 1;
		j = 0;
		i1 = 0;
		j1 = size - 1;
	}

	coordinates[0] = i;
	coordinates[1] = j;
	coordinates[2] = i1;
	coordinates[3] = j1;



		return coordinates;
}

int* Algo::mapCreation(int size){
	int* map = (int*)malloc(size * size * sizeof(int));
	return map;
}

void Algo::freeMap(int* map){
	free(map);
}

