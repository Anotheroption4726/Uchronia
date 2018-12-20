using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Population_script : MonoBehaviour
{
    public static int width = 64;
    public static int height = 32;
    private static string[,] grid = new string[height, width];
    public Vector3Int currentCell;
    public Tilemap tile_population;
    public Tilemap tile_ground_water;
    public Tile tile_skin;
    public TileBase water;
    //  public TileBase ground;
    public static int generation = 0;


    void Start()
    {
        Human_class.population = new List<Human_class>();
        setup();
        Human_class.start();
        print(grid);

    }

    void Update()
    {
        Human_class.update();
        print(generation);
        print(Human_class.population.Count);
        generation++;
    }

    public void display_houses() { 
        for (int i = 0; i < width; i++) { 
            for (int j = 0; j < height; j++) {
                return;
            }
        }
    }   

    public void setup() {
        currentCell = tile_ground_water.WorldToCell(transform.position);
        currentCell.x -= 13;
        currentCell.y -= 5;
        for (int i = 0; i < height; i++) {
            currentCell.x = 0;
            for (int j = 0; j < width; j++)
            {
                if (tile_ground_water.GetTile(currentCell) == water)
                {
                    print("WATER");
                    grid[i,j] = "W";

                }
                else if (tile_ground_water.GetTile(currentCell) != null)
                {
                    print("TERRAIN");
                    grid[i,j] = "T";

                }
                currentCell.x += 1;
            }
            currentCell.y += 1;
        }
    }
}