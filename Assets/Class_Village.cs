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

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    /***************************************************/

    public List<Human_class> habitant = new List<Human_class>();
 

    /***************************************************/

    public static string[,] gridpop = new string[Population_script.height, Population_script.width];

    /***************************************************/

    public List<Vector2Int> vecteur;
    public List<Vector2Int> vecteur_temp;

    /***************************************************/

    public static List<Class_Village> tout_village;
    static Random rd = new Random();

    /***************************************************/


    public Class_Village(int x, int y, Vector2Int vec) { 
        X = x;
        Y = y;
        vecteur = new List<Vector2Int>();
        vecteur_temp = new List<Vector2Int>();
        vecteur.Add(vec);
        this.add_habitant(x, y);
    }

    public void add_habitant(int x, int y) {
        if (this.habitant.Count != 0)
            this.habitant.Clear();
        for (int i = 0; i < Human_class.population.Count; i++)
            if (Human_class.population[i].X == x && Human_class.population[i].Y == y)
                this.habitant.Add(Human_class.population[i]);
    }
  
    public static void setup() {
        for (int i = 0; i < Population_script.height; i++)
            for (int j = 0; j < Population_script.width; j++)
            {
                if (Population_script.check_terrain(j, i) == true)
                    gridpop[i, j] = "0";
                else
                    gridpop[i, j] = "9";
            }
    }

    /***************************************************/

    public static void spawning() {
        if (Population_script.generation % 12 == 0) 
            for (int i = 0; i < tout_village.Count; i++)       
                for (int j = 0; j < tout_village[i].habitant.Count; j++) {
                    tout_village[i].habitant[j].Age++;
                    if (tout_village[i].habitant[j].Age > Population_script.death)
                        tout_village[i].habitant.RemoveAt(j);
                } 
        for (int i = 0; i < tout_village.Count; i++)
            tout_village[i].spawn_pop_village(tout_village[i].X, tout_village[i].Y, i);
        spawn_chateau();
    }

    public  void spawn_pop_village(int x, int y, int index) {
        for (int j = 0; j < habitant.Count; j++)
            if (rd.Next(0, 100) > Population_script.prob_to_born) {
                Human_class pop = new Human_class(x + Population_script.minus_x, y + Population_script.minus_y, -1);
                tout_village[index].habitant.Add(pop);
            }
    }

    public static void spawn_chateau() {
        for (int i = 0; i < Population_script.height; i++)
            for (int j = 0; j < Population_script.width; j++)
                if (Human_class.count_tile(j + Population_script.minus_x, i + Population_script.minus_y) > Population_script.require_pop && gridpop[i, j] == "0") {
                    Vector2Int tempi = new Vector2Int(j, i);
                    Class_Village village = new Class_Village(j + Population_script.minus_x, i + Population_script.minus_y, tempi);
                    tout_village.Add(village);
                    Human_class.supp_civ(j + Population_script.minus_x, i + Population_script.minus_y);
                    gridpop[i, j] = "V";
                }
    }

    /***************************************************/

    public static int retour_pop() {
        int result = 0;
        for (int i = 0; i < tout_village.Count; i++)
            result += tout_village[i].habitant.Count;
        return result;
    }

    /***************************************************/


    public static void expand_castle(int index_village, int index_vecteur) {
        bool good = false;
        do {
            int rand = rd.Next(0, 3);
            Vector2Int vec = new Vector2Int(tout_village[index_village].vecteur[index_vecteur].x, tout_village[index_village].vecteur[index_vecteur].y);
            switch (rand) {
                case 0:
                    if (check_coord(vec.x,vec.y - 1) == true && gridpop[vec.y - 1 , vec.x] == "0")
                    {
                        vec.y--;
                        tout_village[index_village].vecteur.Add(vec);
                        gridpop[vec.y, vec.x] = "V";
                        good = true;
                    }
                    break;

                case 1:
                    if (check_coord(vec.x + 1, vec.y) == true && gridpop[vec.y, vec.x + 1] == "0")
                    {
                        vec.x++;
                        tout_village[index_village].vecteur.Add(vec);
                        gridpop[vec.y, vec.x] = "V";
                        good = true;
                    }
                    break;

                case 2:
                    if (check_coord(vec.x, vec.y + 1) == true && gridpop[vec.y + 1, vec.x] == "0")
                    {
                        vec.y++;
                        tout_village[index_village].vecteur.Add(vec);
                        gridpop[vec.y, vec.x] = "V";
                        good = true;
                    }
                    break;

                case 3:
                    if (check_coord(vec.x - 1, vec.y) == true && gridpop[vec.y, vec.x - 1] == "0")
                    {
                        vec.x--;
                        tout_village[index_village].vecteur.Add(vec);
                        gridpop[vec.y, vec.x] = "V";
                        good = true;
                    }
                    break;

            }
        } while (good == false);
    }

    public static bool check_coord(int x, int y) {
        if ((x < Population_script.width && x >= 0) && (y < Population_script.height && y >= 0))
            return true;
        return false;
    }

    public static void can_expand() {
        for (int i = 0; i < tout_village.Count; i++) {
            if (tout_village[i].vecteur_temp.Count > 0)
                tout_village[i].vecteur_temp.Clear();
            for (int j = 0; j < tout_village[i].vecteur.Count; j++) {
                if (check_castle_neighbour(tout_village[i].vecteur[j]) == true) {
                    tout_village[i].vecteur_temp.Add(tout_village[i].vecteur[j]);
                }
            }
        }
    }

    public static bool check_castle_neighbour(Vector2Int vector)
    {
        if (((check_coord(vector.x, vector.y - 1)) && (gridpop[vector.y - 1, vector.x] == "0"))
         || ((check_coord(vector.x, vector.y + 1)) && (gridpop[vector.y + 1, vector.x] == "0"))
         || ((check_coord(vector.x + 1, vector.y)) && (gridpop[vector.y, vector.x + 1] == "0"))
         || ((check_coord(vector.x - 1, vector.y)) && (gridpop[vector.y, vector.x - 1] == "0")))
            return true;
        return false;
    }
}
