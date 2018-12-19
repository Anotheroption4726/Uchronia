using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class main : MonoBehaviour
{
    public Vector3Int tile_position = new Vector3Int(0, 0, 1);
    public Tilemap tile_map;
    public Tile tile_skin;
    public static int generation = 0;

    void Start()
    {
        Human_class.population = new List<Human_class>();
        Human_class.start();

    }

    void Update()
    {
        //Thread.Sleep(2000);

         if (Human_class.count_tile(0, 0) == 20)
         {
             tile_map.SetTile(tile_position, tile_skin);
         }
        Human_class.update();
        //tile_map.SetTile(tile_position, tile_skin);
        print(generation);
        print(Human_class.population.Count);
        generation++;
    }


}