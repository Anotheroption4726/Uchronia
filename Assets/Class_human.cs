using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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

    public static void start()
    {
        for (int i = 0; i < 10; i++)
        {
            Human_class pop = new Human_class(0, 0, 10);
            population.Add(pop);
        }
    }


   public static void update()
    {
        System.Random rd = new System.Random();
        if (rd.Next(1, 35) % 2 == 0)
        {
            Human_class pop = new Human_class(0, 0, 10);
            population.Add(pop);
        }
        for (int i = 0; i < population.Count; i++)
        {
            population[i].Age++;
            if (population[i].Age > 70)
                population.RemoveAt(i);
        }
    }
}
