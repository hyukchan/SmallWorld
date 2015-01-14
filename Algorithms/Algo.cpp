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

#define IMPOSSIBLE 0
#define POSSIBLE 1

#define MVTPT 1

#define NAIN 0
#define ELF 1
#define ORC 2

/*
  Constructeur
  */
Algo* Algo::Algo_new()
{
	return new Algo();
}
/*
Destructeur
*/
void Algo::delete_Algo(Algo* algo){
	delete algo;
}

/*
Crée le tableau représentant le plateau de jeu
@param n La largeur du tableau
@return Le tableau
*/
int* Algo::createGameBoard(int n){
	srand(time(NULL));

	int* tiles = new int[n*n];

	int initNbTiles = n*n / 4;

	int LeftTiles[] = { initNbTiles, initNbTiles, initNbTiles, initNbTiles };

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

/*
Place les unités en début de partie
@param map le tableau représentant la carte
@param size la taille de la carte
@return Le tableau contenant les abscisses et ordonnées
*/
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

/*
 Calcule le tableau contenant les déplacements possibles
 @param movePt Les points de déplacement de l'unité
 @param moves le tableau de déplacements
 @param x l'abscisse de l'unité courante
 @param y l'ordonnée de l'unité courante
 @return Le tableau contenant 1 si le déplacement est possible, 0 sinon
 */
int* Algo::possibleMoves(double movePt, int* moves, int x, int y, int size, int* map, int peuple)
{
	int i, j;
	bool pair = y % 2 == 0;
	//reinitialisation tableau
	for (i = 0; i < size; i++){
		for (j = 0; j < size; j++){
			moves[i * size + j] = IMPOSSIBLE;
		}
	}

	//cas pair et movePt = 1
	if (movePt == 1){
		if (x != 0){
			//case gauche
			if (peuple == ELF && map[y*size + x - 1] == DESERT){
				moves[y*size + x - 1] = IMPOSSIBLE;
			}
			else{
				moves[y*size + x - 1] = POSSIBLE;
			}
			//cases dessus
			if (y != 0){
				//dessus gauche pair
				if (pair){
					if (peuple == ELF && map[(y - 1)*size + x - 1] == DESERT){
						moves[(y-1)*size + x - 1] = IMPOSSIBLE;
					}
					else{
						moves[(y - 1)*size + x - 1] = POSSIBLE;
					}
				}
				if (peuple == ELF && map[(y - 1)*size + x] == DESERT){ moves[y*size + x - 1] = IMPOSSIBLE; }
				else{
					moves[(y - 1)*size + x] = POSSIBLE;
				}
			}
			//cases du dessous
			if (y != size){
				if (peuple == ELF && map[(y + 1)*size + x] == DESERT){ 
					moves[y*size + x - 1] = IMPOSSIBLE; 
				}
				else{
					moves[(y + 1)*size + x] = POSSIBLE;
				}
				//dessous gauche pair
				if (pair){
					if (peuple == ELF && map[(y + 1)*size + x - 1] == DESERT){ 
					moves[y*size + x - 1] = IMPOSSIBLE; 
				}
					else{
						moves[(y + 1)*size + x - 1] = POSSIBLE;
					}
				}
			}
		}
		//case de droite
		if (x != size - 1){
			if (peuple == ELF && map[y*size + x + 1] == DESERT){ 
				moves[y*size + x - 1] = IMPOSSIBLE; 
			}
			else{
				moves[y*size + x + 1] = POSSIBLE;
			}
			//case dessus droite impair
			if (y != 0 && !pair){
				if (peuple == ELF && map[(y - 1)*size + x + 1] == DESERT){ 
					moves[y*size + x - 1] = IMPOSSIBLE; 
				}
				else{
					moves[(y-1)*size + x + 1] = POSSIBLE;
				}
			}
			//case dessous droite impair
			if (y != size - 1 && !pair){
				if (peuple == ELF && map[(y - 1)*size + x + 1] == DESERT){ 
					moves[y*size + x - 1] = IMPOSSIBLE; 
				}
				else{
					moves[(y + 1)*size + x + 1] = POSSIBLE;
				}
			}
		}
	}
	return moves;
}


/*
Crée un tableau d'entiers
@param size La largeur du tableau
@return Le tableau
*/
int* Algo::mapCreation(int size){
	int* map = (int*)malloc(size * size * sizeof(int));
	int i;
	for (i = 0; i < size * size; i++){
		map[i] = IMPOSSIBLE;
	}
	return map;
}

/*
Libère l'espace alloué à un tableau d'entiers
*/
void Algo::freeMap(int* map){
	free(map);
}

