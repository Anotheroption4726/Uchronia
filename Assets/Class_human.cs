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

    public static void supp_civ(int x, int y) {
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].X == x && population[i].Y == y)
            {
                population.RemoveAt(i);
            }
        }
    }

    public static void olding_pop(int x, int y)
    {
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].X == x && population[i].Y == y)
            {
                population[i].Age++;
                if (population[i].Age == Population_script.death)
                    population.RemoveAt(i);
            }
        }
    }

    public static void spawning_alone() {
        for (int i = 0; i < Population_script.height; i++) {
            for (int j = 0; j < Population_script.width; j++)
            {
                int temp = count_tile(j + Population_script.minus_x, i + Population_script.minus_y);
                if (temp > 1 && temp < Population_script.require_pop + 1)
                {
                    for (int k = 0; k < temp; k++)
                    {
                        if (k % 2 == 0)
                        {
                            population.Add(new Human_class(j + Population_script.minus_x, i + Population_script.minus_y, -1));
                        }
                    }
                    olding_pop(j + Population_script.minus_x, i + Population_script.minus_y);
                }

            }
        }
        return;
    }
}
