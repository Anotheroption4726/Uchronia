﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = System.Random;

public class Class_Village 
{
    private int x;
    private int y;
    public List<Human_class> habitant = new List<Human_class>();
    public static string[,] gridpop = new string[Population_script.height, Population_script.width];
    public List<Vector2Int>vecteur;
    public static List<Class_Village> tout_village;
    public static bool spawn = false;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    public Class_Village(int x, int y, Vector2Int vec) { 
        X = x;
        Y = y;
        vecteur = new List<Vector2Int>();
        vecteur.Add(vec);
        this.add_habitant(x, y);
    }


    public void add_habitant(int x, int y) {
        if (this.habitant.Count != 0)
        {
            this.habitant.Clear();
        }
        for (int i = 0; i < Human_class.population.Count; i++)
        {
            if (Human_class.population[i].X == x && Human_class.population[i].Y == y)
            {
                this.habitant.Add(Human_class.population[i]);
            }
        }
        return;
    }
   
    public static void check_village() {
        return;
    }
    public static void setup()
    {
        for (int i = 0; i < Population_script.height; i++)
        {
            for (int j = 0; j < Population_script.width - 1; j++)
            {
                gridpop[i, j] = "0";
            }
        }
        return;
    }
}
