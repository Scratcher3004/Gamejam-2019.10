using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Generator
{

    // 0 = OL, 1 = OR, 2 = R, 3 = UR, 4 = UL, 5 = L

    private static List<Tile> standardtiles = new List<Tile>();
    static Tile Tile0;
    static Tile Tile1;
    static Tile Tile2;
    static Tile Tile3;
    static Tile Tile4;
    static Tile Tile5;
    static Tile Tile6;
    static Tile Tile7;
    static Tile Tile8;
    static Tile Tile9;

    public static void InitStandardTiles()
    {
        Tile0 = new Tile {OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile0"};
        Tile1 = new Tile {OO = 1, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile1"};
        Tile2 = new Tile {OO = 2, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile2"};
        Tile3 = new Tile {OO = 3, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile3"};
        Tile4 = new Tile {OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile4"};
        Tile5 = new Tile {OO = 3, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile5"};
        Tile6 = new Tile {OO = 2, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile6"};
        Tile7 = new Tile {OO = 2, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile7"};
        Tile8 = new Tile {OO = 1, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile8"};
        Tile9 = new Tile {OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0, Name = "Tile9"};
        standardtiles.Add(Tile0);
        standardtiles.Add(Tile1);
        standardtiles.Add(Tile2);
        standardtiles.Add(Tile3);
        standardtiles.Add(Tile4);
        standardtiles.Add(Tile5);
        standardtiles.Add(Tile6);
        standardtiles.Add(Tile7);
        standardtiles.Add(Tile8);
        standardtiles.Add(Tile9);
    }


    public static Tile[,] GetTileMap(int width, int height, int seed)
    {
        System.Random zufall;
        if (seed == 0)
            zufall = new System.Random();
        else
            zufall = new System.Random(seed);
        
        Tile[,] result = new Tile[width, height];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                result[x, y] = standardtiles[zufall.Next(0,10)];
            }
        }

        return result;
    }


}
public class Tile
{

[Serializable]
public class Tile
{
    // 0 = Land, 1 = Block, 2= Road, 3= Water
    public int OO;
    public int OL;
    public int UL;
    public int OR;
    public int UR;
    public int UU;
    public string Name { get; set; }
    
    
}

