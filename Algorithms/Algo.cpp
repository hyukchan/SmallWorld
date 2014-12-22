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

int* startingPositions(int* map, int size){

	srand((unsigned int)time(NULL));

	int* coordinates = (int*)malloc(4 * sizeof(int));

	int pos = rand() % 2;
	int i, j, i1, j1;
	if (pos == 1){
		i = rand() % 2;
		j = size - rand() % 2;
		i1 = size - rand() % 2;
		j1 = rand() % 2;
	}
	else
	{
		i = rand() % 2;
		j = rand() % 2;
		i1 = size - rand() % 2;
		j1 = size - rand() % 2;
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

double* costTab(int size)
{
	double* tab = (double*)malloc(size * size * sizeof(double));
	return tab;
}

void freeCost(double* costTab)
{
	free(costTab);
}

/*

D�placements simples des unit�s

*/

//d�placements orc

void Algo::initializeOrcMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{
	//Par d�faut toute les cases sont inacessibles
	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}
	//Les points de d�placements restants sont initialis�s � 0
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

void Algo::orcPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{
		//case au dessus a gauche
		if (moves[(x - 1)*size + y] == INIT){
			orcMovement(map, cost, size, (x - 1), y, cost[(x - 1)*size + y], moves);
			orcPossibleMovement2(map, size, (x - 1), y, moves, cost);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x - 1)*size + (y + 1)] == INIT)
			{
				orcMovement(map, cost, size, (x - 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
				orcPossibleMovement2(map, size, (x - 1), y, moves, cost);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y - 1)] == INIT){
			orcMovement(map, cost, size, x, (y - 1), cost[x*size + (y - 1)], moves);
			orcPossibleMovement2(map, size, x, (y - 1), moves, cost);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y + 1)] == INIT){
			orcMovement(map, cost, size, x, (y + 1), cost[x*size + (y + 1)], moves);
			orcPossibleMovement2(map, size, x, (y + 1), moves, cost);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			orcMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			orcPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			orcMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			orcPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}
	}
}

void Algo::orcPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{
		//case au dessus a gauche
		if (moves[(x - 1)*size + y] == INIT){
			orcMovement(map, cost, size, (x - 1), y, cost[(x - 1)*size + y], moves);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x - 1)*size + (y + 1)] == INIT)
			{
				orcMovement(map, cost, size, (x - 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y - 1)] == INIT){
			orcMovement(map, cost, size, x, (y - 1), cost[x*size + (y - 1)], moves);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y + 1)] == INIT){
			orcMovement(map, cost, size, x, (y + 1), cost[x*size + (y + 1)], moves);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			orcMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			orcMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}
	}
}
void Algo::orcMovement(int* map, double* cost, int size,int x, int y, double movPt, int* moves)
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

// d�placements elfs

void Algo::initializeElfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{
	//Par d�faut toute les cases sont inacessibles
	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}
	//Les points de d�placements restants sont initialis�s � 0
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

void Algo::elfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{
		//case au dessus a gauche
		if (moves[(x - 1)*size + y] == INIT){
			elfMovement(map, cost, size, (x - 1), y, cost[(x - 1)*size + y], moves);
			elfPossibleMovement2(map, size, (x - 1), y, moves, cost);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x - 1)*size + (y + 1)] == INIT)
			{
				elfMovement(map, cost, size, (x - 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
				elfPossibleMovement2(map, size, (x - 1), y, moves, cost);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y - 1)] == INIT){
			elfMovement(map, cost, size, x, (y - 1), cost[x*size + (y - 1)], moves);
			elfPossibleMovement2(map, size, x, (y - 1), moves, cost);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y + 1)] == INIT){
			elfMovement(map, cost, size, x, (y + 1), cost[x*size + (y + 1)], moves);
			elfPossibleMovement2(map, size, x, (y + 1), moves, cost);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			elfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			elfPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			elfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			elfPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}
	}
}

void Algo::elfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{
		//case au dessus a gauche
		if (moves[(x - 1)*size + y] == INIT){
			elfMovement(map, cost, size, (x - 1), y, cost[(x - 1)*size + y], moves);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x - 1)*size + (y + 1)] == INIT)
			{
				elfMovement(map, cost, size, (x - 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y - 1)] == INIT){
			elfMovement(map, cost, size, x, (y - 1), cost[x*size + (y - 1)], moves);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y + 1)] == INIT){
			elfMovement(map, cost, size, x, (y + 1), cost[x*size + (y + 1)], moves);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			elfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			elfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}
	}
}

void Algo::elfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves)
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

// d�placements nains
void Algo::initializeDwarfMvt(int * map, int size, int x, int y, double * cost, int * moves, double movPt)
{

	int i, j;
	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = INIT;
		}
	}

	for (i = 0; i < size; i++)
	{
		for (j = 0; j < size; j++)
		{
			moves[i*size + j] = 0;
		}
	}

	moves[x*size + y] = INIT;
	cost[x*size + y] = movPt;
	dwarfPossibleMovement(map, size, x, y, moves, cost);
}

void Algo::dwarfPossibleMovement(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{   
		//case au dessus a gauche
		if(moves[(x-1)*size + y] == INIT){
			dwarfMovement(map, cost, size, (x-1), y, cost[(x-1)*size+y], moves);
			dwarfPossibleMovement2(map, size, (x-1), y, moves, cost);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x-1)*size + (y+1)] == INIT)
			{
				dwarfMovement(map, cost, size, (x-1), (y+1), cost[(x-1)*size + (y+1)], moves);
				dwarfPossibleMovement2(map, size, (x - 1), y, moves, cost);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y-1)] == INIT){
			dwarfMovement(map, cost, size, x, (y-1), cost[x*size + (y-1)], moves);
			dwarfPossibleMovement2(map, size, x, (y-1), moves, cost);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y+1)] == INIT){
			dwarfMovement(map, cost, size, x, (y+1), cost[x*size + (y+1)], moves);
			dwarfPossibleMovement2(map, size, x, (y+1), moves, cost);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			dwarfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			dwarfPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			dwarfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			dwarfPossibleMovement2(map, size, (x + 1), (y - 1), moves, cost);
		}
	}
}

void Algo::dwarfPossibleMovement2(int* map, int size, int x, int y, int* moves, double* cost)
{
	//cases au dessus
	if (x != 0)
	{
		//case au dessus a gauche
		if (moves[(x - 1)*size + y] == INIT){
			dwarfMovement(map, cost, size, (x - 1), y, cost[(x - 1)*size + y], moves);
		}
		// case au dessus � droite
		if (y != size - 1)
		{
			if (moves[(x - 1)*size + (y + 1)] == INIT)
			{
				dwarfMovement(map, cost, size, (x - 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
			}
		}
	}

	if (y != 0)
	{
		// case de gauche
		if (moves[x*size + (y - 1)] == INIT){
			dwarfMovement(map, cost, size, x, (y - 1), cost[x*size + (y - 1)], moves);
		}
	}

	if (y != size - 1)
	{
		// case de droite
		if (moves[x*size + (y + 1)] == INIT){
			dwarfMovement(map, cost, size, x, (y + 1), cost[x*size + (y + 1)], moves);
		}
	}
	//case au dessous
	if (x = !size - 1)
	{
		// case dessous � gauche
		if (moves[(x + 1)*size + y - 1] == INIT)
		{
			dwarfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}

		//case dessous � droite
		if (moves[(x + 1)*size + (y - 1)] == INIT)
		{
			dwarfMovement(map, cost, size, (x + 1), (y + 1), cost[(x - 1)*size + (y + 1)], moves);
		}
	}
}


void Algo::dwarfMovement(int* map, double* cost, int size, int x, int y, double movPt, int* moves)
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

