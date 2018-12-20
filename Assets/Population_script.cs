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
    public  string[,] grid = new string[height, width];
    public  Vector3Int currentCell;
    public  Tilemap tile_ground_water;
    public  Tilemap tile_population;
    public  TileBase tile_skin;
    public  TileBase water;
    public static int generation = 0;
    public static int x;
    public static int y;
    System.Random rd = new System.Random();


    void Start()
    {
        Human_class.population = new List<Human_class>();
        Class_Village.tout_village = new List<Class_Village>();
        setup();
        Human_class.setup();
        Class_Village.setup();

    }

    void Update()
    {
        int x = rd.Next(0, 64);
        int y = rd.Next(0, 32);
        if (grid[y, x] == "T")
        {
                Human_class.spawning(x, y);
                print(Human_class.count_tile(x - 36, y - 16));
            }
            if (Class_Village.spawn == true)
            {

                tile_population.SetTile(new Vector3Int(Class_Village.tout_village[Class_Village.tout_village.Count - 1].X, Class_Village.tout_village[Class_Village.tout_village.Count - 1].Y, -1), tile_skin);
                Class_Village.spawn = false;
            }/*
        print(generation);*/
             // print(Human_class.population.Count);
            generation++;
        }

        public void setup() {
            currentCell = tile_ground_water.WorldToCell(transform.position);
            currentCell.x = -34;
            currentCell.y = -16;
            for (int i = 0; i < height; i++) {
                currentCell.x = -34;
                for (int j = 0; j < width; j++)
                {
                    if (tile_ground_water.GetTile(currentCell) == water)
                        grid[i, j] = "W";

                    else if (tile_ground_water.GetTile(currentCell) != null)
                        grid[i, j] = "T";

                    currentCell.x += 1;
                }
                currentCell.y += 1;
            }
        }
    
    
}