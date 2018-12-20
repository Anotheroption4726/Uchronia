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

    Human_class(int x, int y, int age)
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
        for (int i = 0; i < 10; i++)
        {
            Human_class pop = new Human_class(0, 0, 0);
            population.Add(pop);
        }
    }


    public static void spawning(int x, int y)
    {
    
      

            Human_class pop = new Human_class(x -36 , y - 16, 0);
            population.Add(pop);
            if (Population_script.generation % 30 == 0)
            {
                for (int i = 0; i < population.Count; i++)
                {
                    population[i].Age++;
                    /*if (population[i].Age > 400)
                        population.RemoveAt(i);*/
                }
            }
        if (population.Count > 10)
        {
            for (int i = 0; i < Population_script.height; i++)
            {
                for (int j = 0; j < Population_script.width - 1; j++)
                {
                    if (Human_class.count_tile(j - 34, i - 16) > 5 && Class_Village.gridpop[i, j] == "0")
                    {
                        Vector2Int tempi = new Vector2Int(j, i);
                        Vector3Int tempo = new Vector3Int(i - 36 , j - 16, -1);
                        Class_Village village = new Class_Village(j - 36, i -16, tempi);
                        Class_Village.tout_village.Add(village);
                        Class_Village.gridpop[i, j] = "V";
                        Class_Village.spawn = true;

                    }
                }
            }
        }  
    }
}
