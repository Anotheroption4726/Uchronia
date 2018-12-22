using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class Population_script : MonoBehaviour
{

    /***************************************************/  

    public static int width = 64;
    public static int height = 32;
    public static int minus_x = -34;
    public static int minus_y = -16;
    public static int time_to_wait = 1000;
    public static int initialisation_population = 200;
    public static int require_pop = 40;
    public static int death = 10;


    /***************************************************/


    public string[,] grid = new string[height, width];
    public Vector3Int currentCell;

    /***************************************************/

    public  Tilemap tile_ground_water;
    public  Tilemap tile_population;
    public  TileBase tile_skin;
    public  TileBase water;

    /***************************************************/

    public Text display_pop;
    public Text display_gene;
    public Text display_village;
    public Text display_nomade;

    /***************************************************/


    public static int generation = 0;
    System.Random rd = new System.Random();


    /***************************************************/

    void Start()
    {
        setup();
    }

    /***************************************************/

    public void display_canvas() {
        display_pop.text = "Population : " + (Class_Village.retour_pop() + Human_class.population.Count).ToString();
        display_gene.text = "Generations : " + generation.ToString();
        display_village.text = "Villages : " + Class_Village.tout_village.Count.ToString();
        display_nomade.text = "Nomades : " + Human_class.population.Count.ToString();
    }

    public void display_chateau()
    {

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (Class_Village.gridpop[i, j] == "V")
                    tile_population.SetTile(new Vector3Int(j + minus_x, i + minus_y, -1), tile_skin);

            }
        }
    }

    /***************************************************/

    public void setup() {
        Human_class.population = new List<Human_class>();
        Class_Village.tout_village = new List<Class_Village>();
        map_to_ascii();
        initialisation_pop();
        Class_Village.setup();
    }

    public void map_to_ascii() { 
        currentCell = tile_ground_water.WorldToCell(transform.position);
        currentCell.x = minus_x;
        currentCell.y = minus_y;
        for (int i = 0; i<height; i++) {
                currentCell.x = minus_x;
                for (int j = 0; j<width; j++)
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

    public void initialisation_pop() {
        int x;
        int y;
        int h = 0;
        while (h++ < initialisation_population)
        {
            x = rd.Next(0, width);
            y = rd.Next(0, height);
            if (grid[y, x] == "T")
            {
                Human_class pop = new Human_class(x + minus_x, y + minus_y, -1);
                Human_class.population.Add(pop);
            }

        }
    }

    /***************************************************/

    void Update()
    {
        spawn();
        display_canvas();
        generation++;
        //Thread.Sleep(time_to_wait);
    }

    /***************************************************/

    public void spawn()
    {
        Human_class.spawning_alone();
        Class_Village.spawning();
        display_chateau();
    }

}