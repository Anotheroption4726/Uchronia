using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Human_class
{

    private int x;
    private int y;
    private int age;
    public static List<Human_class> population;
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Age { get => age; set => age = value; }

    public Human_class(int x, int y, int age)
    {
        X = x;
        Y = y;
        Age = age;
    }

    public static int count_tile(int x, int y)
    {
        int result = 0;
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].X == x && population[i].Y == y)
            {
                result++;
            }
        }
        return result;
    }

    public static void setup()
    {
        return;
    }


    public static void spawning()
    {
        if (Population_script.generation % 12 == 0)
        {
            for (int i = 0; i < population.Count; i++)
            {
                population[i].Age++;
                if (population[i].Age > 90)
                    population.RemoveAt(i);
            }
        }
        for (int i = 0; i < Class_Village.tout_village.Count; i++)
        {
            Class_Village.tout_village[i].spawn_pop_village(Class_Village.tout_village[i].X , Class_Village.tout_village[i].Y, i);
        }
        spawn_chateau();
    }
    public static void spawn_chateau() {
         for (int i = 0; i < Population_script.height; i++)
            {
                for (int j = 0; j < Population_script.width; j++)
                {
                    if (Human_class.count_tile(j + Population_script.minus_x, i + Population_script.minus_y) > 15 && Class_Village.gridpop[i, j] == "0")
                    {
                    Vector2Int tempi = new Vector2Int(j, i);
                        Class_Village village = new Class_Village(j + Population_script.minus_x, i + Population_script.minus_y, tempi);
                        Class_Village.tout_village.Add(village);
                        Class_Village.gridpop[i, j] = "V";
                    }
                }
            }
    }
}
