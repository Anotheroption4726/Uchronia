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
    public List<Human_class> habitant;
    public List<Vector3Int> vecteur;
    public static List<Class_Village> tout_village;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    Class_Village(int x, int y, Vector3Int vec) {
        X = x;
        Y = y;
        vecteur = new List<Vector3Int>();
        vecteur.Add(vec);
        this.add_habitant(x, y);
    }


    public void add_habitant(int x, int y) {
        if (habitant.Count != 0)
        {
            habitant.Clear();
        }
        for (int i = 0; i < Human_class.population.Count; i++)
        {
            if (Human_class.population[i].X == x && Human_class.population[i].Y == y)
            {
                habitant.Add(Human_class.population[i]);
            }
        }
        return;
    }
    public Vector3Int retour_direction() {
        Vector3Int i = new Vector3Int();
        Random rd = new Random();
        int j = rd.Next(0, 3);
        switch (j)
        {
            case 0:
            default:
                break;
        }
        return i;
    }

    public static void check_village() {
        return;
    }
}
