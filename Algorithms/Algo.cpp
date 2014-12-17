#include "Algo.h"
#include <iostream>
#include <stdio.h>
#include <stdlib.h> 
#include <time.h>
using namespace std;

#define PLAIN 0
#define DESERT 1
#define FOREST 2
#define MOUNTAIN 3

#define INIT 0
#define POSSIBLE 1
#define IMPOSSIBLE 2

/**
* Constructeur
*/
Algo* Algo_new()
{
	return new Algo();
}
/**
* Destructeur
*/
void delete_Algo(Algo* algo){
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

int* startingPositions(int* map, int n){

	srand((unsigned int)time(NULL));

	int* coordinates = (int*)malloc(4 * sizeof(int));

	int pos = rand() % 2;
	int i, j, i1, j1;
	if (pos == 1){
		i = rand() % 2;
		j = n - rand() % 2;
		i1 = n - rand() % 2;
		j1 = rand() % 2;
	}
	else
	{
		i = rand() % 2;
		j = rand() % 2;
		i1 = n - rand() % 2;
		j1 = n - rand() % 2;
	}

	coordinates[0] = i;
	coordinates[1] = j;
	coordinates[2] = i1;
	coordinates[3] = j1;



		return coordinates;
}

int* mapCreation(int size){
	int* map = (int*)malloc(size * size * sizeof(int));
	return map;
}

void freeMap(int* map){
	free(map);
}

double* CostTab(int size)
{
	double* tab = (double*)malloc(size * size * sizeof(double));
	return tab;
}

void freeCost(double* costTab)
{
	free(costTab);
}

/*

Déplacements simples des unités

*/

//déplacements orc

void initializeOrcMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{
	//Par défaut toute les cases sont inacessibles
	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}
	//Les points de déplacements restants sont initialisés à 0
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = 0;
		}
	}
	//On autorise la case initiale
	moves[x*size + y] = INIT;
	cost[x*size + y] = movPt;
	orcPossibleMovement(map, size, x, y, moves, cost);
}

void orcPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{

}

void orcMovement(int* map, double* cost, int size,int x, int y, double movPt, int* moves)
{
	double mvt = movPt;

	switch (map[size*x + y])
	{
	case PLAIN :
		if (mvt >= 0.5){
			mvt = mvt - 0.5;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;

	case MOUNTAIN :
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case FOREST :
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case DESERT :
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	default :
		break;
	}


}

// déplacements elfs

void initializeElfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{
	//Par défaut toute les cases sont inacessibles
	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}
	//Les points de déplacements restants sont initialisés à 0
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = 0;
		}
	}
	//On autorise la case initiale
	moves[x*size + y] = INIT;
	cost[x*size + y] = movPt;
	elfPossibleMovement(map, size, x, y, moves, cost);
}

void elfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{

}
void elfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves)
{
	double mvt = movPt;

	switch (map[size*x + y])
	{
	case FOREST :
		if (mvt >= 0.5){
			mvt = mvt - 0.5;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;

	case MOUNTAIN:
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case PLAIN :
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case DESERT:
			moves[x*size + y] = IMPOSSIBLE;
		break;
	default:
		break;
	}

}

// déplacements nains
void initializeDwarfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{
	//Par défaut toute les cases sont inacessibles
	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}
	//Les points de déplacements restants sont initialisés à 0
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = 0;
		}
	}
	//On autorise la case initiale
	moves[x*size + y] = INIT;
	cost[x*size + y] = movPt;
	dwarfPossibleMovement(map, size, x, y, moves, cost);
}

void dwarfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{

}
void dwarfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves)
{
	double mvt = movPt;

	switch (map[size*x + y])
	{
	case PLAIN :
		if (mvt >= 0.5){
			mvt = mvt - 0.5;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;

	case MOUNTAIN:
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case FOREST:
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	case DESERT:
		if (mvt >= 1){
			mvt--;
			moves[x*size + y] = POSSIBLE;
			cost[x*size + y] = mvt;
		}
		else{
			moves[x*size + y] = INIT;
		}
		break;
	default:
		break;
	}

}
// TODOSW Déplacement optimal

