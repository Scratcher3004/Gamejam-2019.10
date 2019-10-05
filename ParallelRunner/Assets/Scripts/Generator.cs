using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Generator
{

    // 0 = OL, 1 = OR, 2 = R, 3 = UR, 4 = UL, 5 = L

    private static List<Tile> standardtiles = new List<Tile>();
    public static Tile Tile0 { get; set; };
    public static Tile Tile1;
    public static Tile Tile2;
    public static Tile Tile3;
    public static Tile Tile4;
    public static Tile Tile5;
    public static Tile Tile6;
    public static Tile Tile7;
    public static Tile Tile8;
    public static Tile Tile9;

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
                
                if ((x == 0) || (y == 0) || (x == height - 1) || (y == width - 1))
                {
                    result[x, y] = Tile3;
                }
                else
                {
                    result[x, y] = standardtiles[zufall.Next(0,10)];
                }
            }
        }
        
    
        
        

        return result;
    }


}

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

    public override bool Equals(object obj)
    {
        Tile t = null;
        try
        {
            t = (Tile)obj;
        }
        catch (Exception e)
        {
            return false;
        }

        if (t == null)
            return false;
        
        return OO == t.OO && OL == t.OL && UL == t.UL && OR == t.OR && UR == t.UR && UU == t.UU;
    }

    public string Name { get; set; }
    
    
}