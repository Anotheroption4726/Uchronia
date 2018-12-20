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
    public TileBase terrain;
    public static int generation = 0;


    void Start()
    {
        Human_class.population = new List<Human_class>();
        Class_Village.tout_village = new List<Class_Village>();
        setup();
        Human_class.setup();
    }

    void Update()
    {
        Human_class.spawning();
        Class_Village.check_village();
        print(generation);
        print(Human_class.population.Count);
        generation++;
    }

    public void setup() {
        currentCell = tile_ground_water.WorldToCell(transform.position);
        currentCell.x = -34;
        currentCell.y = -16;
        for (int i = 0; i < height; i++) {
            currentCell.x = -34;
            for (int j = 0; j < width - 1; j++)
            {
                if (tile_ground_water.GetTile(currentCell) == water)     
                    grid[i, j] = "W";
           
                else if (tile_ground_water.GetTile(currentCell) == terrain || tile_ground_water.GetTile(currentCell) != null)        
                    grid[i, j] = "T";
                
                currentCell.x += 1;
            }
            currentCell.y += 1;
        }
    }
}