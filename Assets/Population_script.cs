using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Population_script : MonoBehaviour
{
    public static int width = 64;
    public static int height = 32;
    public static int minus_x = -34;
    public static int minus_y = -16;
    public  string[,] grid = new string[height, width];
    public  Vector3Int currentCell;
    public  Tilemap tile_ground_water;
    public  Tilemap tile_population;
    public  TileBase tile_skin;
    public  TileBase water;
    public  Text display_pop;
    public Text display_gene;
    public Text display_village;
    public static int generation = 0;
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
        Thread.Sleep(1000);
        spawn();
        display_pop.text = "Population : " + Human_class.population.Count.ToString();
        display_gene.text = "Generation : " + generation.ToString();
        display_village.text = "Villages : " + Class_Village.tout_village.Count.ToString();
        generation++;
    }

    public void spawn() {
        Human_class.spawning();
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                if (Class_Village.gridpop[i, j] == "V")
                    display_chateau(j + minus_x, i + minus_y, -1);
            }
        }
    }

    public void display_chateau(int x, int y, int z) {
        tile_population.SetTile(new Vector3Int(x, y, z), tile_skin);
    }

    public void setup() {
        int x;
        int y;
        int h = 0;
        currentCell = tile_ground_water.WorldToCell(transform.position);
        currentCell.x = minus_x;
        currentCell.y = minus_y;
        for (int i = 0; i < height; i++) {
                currentCell.x = minus_x;
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
        while (h++ < 5)
        {
            x = rd.Next(0, width);
            y = rd.Next(0, height);
            if (grid[y, x] == "T")
            {
                for (int i = 0; i < 20; i++)
                {
                    Human_class pop = new Human_class(x + minus_x, y + minus_y, 0);
                    Human_class.population.Add(pop);
                }
            }
        }

    }
}