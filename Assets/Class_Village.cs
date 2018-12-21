using System.Collections;
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
    Random rd = new Random();

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
  
    public static void setup()
    {
        for (int i = 0; i < Population_script.height; i++)
        {
            for (int j = 0; j < Population_script.width ; j++)
            {
                gridpop[i, j] = "0";
            }
        }
        return;
    }

    public  void spawn_pop_village(int x, int y, int index)
    {
            for (int j = 1; j < habitant.Count; j++)
            {
                if (rd.Next(1, j) % rd.Next(1, j) == 0)
                {
                    Human_class pop = new Human_class(x + Population_script.minus_x, y + Population_script.minus_y, 0);
                    tout_village[index].habitant.Add(pop);
                }
            }
       
    }

    public static void spawning()
    {
        if (Population_script.generation % 12 == 0)
        {
            for (int i = 0; i < tout_village.Count; i++)
            {
                for (int j = 0; j < tout_village[i].habitant.Count; j++)
                {
                    tout_village[i].habitant[j].Age++;
                    if (tout_village[i].habitant[j].Age > Population_script.death)
                       tout_village[i].habitant.RemoveAt(j);
                }
            }
        }
        for (int i = 0; i < tout_village.Count; i++)
            tout_village[i].spawn_pop_village(tout_village[i].X, tout_village[i].Y, i);
        spawn_chateau();
    }

    public static void spawn_chateau()
    {
        for (int i = 0; i < Population_script.height; i++)
        {
            for (int j = 0; j < Population_script.width; j++)
            {
                if (Human_class.count_tile(j + Population_script.minus_x, i + Population_script.minus_y) > Population_script.require_pop && gridpop[i, j] == "0")
                {
                    Vector2Int tempi = new Vector2Int(j, i);
                    Class_Village village = new Class_Village(j + Population_script.minus_x, i + Population_script.minus_y, tempi);
                    tout_village.Add(village);
                    Human_class.supp_civ(j + Population_script.minus_x, i + Population_script.minus_y);
                    gridpop[i, j] = "V";
                }
            }
        }
    }

    public static int retour_pop() {
        int result = 0;
        for (int i = 0; i < tout_village.Count; i++) {
            result += tout_village[i].habitant.Count;
        }
        return result;
    }
}
