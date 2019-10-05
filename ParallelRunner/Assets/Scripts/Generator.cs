using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class Generator
{

    // Land = 0, Blockade = 1, Road = 2, Wasser = 3
    private static int Land = 0;

    private static int Blockade = 1;

    private static int Road = 2;

    private static int Wasset = 3;
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
        Tile3 = new Tile {OO = 1, OL = 1, OR = 1, UL = 1, UR = 1, UU = 1, Name = "Tile3"};
            
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
                    Tile t = GenerateTile(result, x, y, width);
                    if (t == null)
                        throw new Exception("No Tile found!");
                    result[x, y] = t;
                }
            }
        }
        return result;
    }

    private static Tile GenerateTile(Tile[,] tileMap, int posX, int posY, int width)
    {
        Tile result = null;
        int counter = 0;
        bool gefunden = false;
        System.Random zufall = new Random();
        while ((gefunden == false) && (counter < 30))
        {
            Tile t = standardtiles[zufall.Next(0,9)];
            bool res1 = IsTileCompatibleOO(t, posX, posY, tileMap);
            bool res2 = IsTileCompatibleOL(t, posX, posY, tileMap);
            bool res3 = IsTileCompatibleOR(t, posX, posY, tileMap,width);
            if (res1 && res2 && res3)
            {
                result = t;
                gefunden = true;
                break;
            }
        }
        return result;
    }

//    private bool IsTileCompatible(Tile[,] tilemap, Tile originalTile, int direction, int tilexpos, int tileypos, int height, int width)
//    {
//        bool result = false;
//        switch (direction)
//        {
//            case 2:
//                if (tileypos > 0)
//                {
//                    if (tilexpos < width)
//                    {
//                        result = AreTilesCompatible(originalTile.OL, 
//                            tilemap[tilexpos,tileypos - 1]);
//                    }
//                }
//
//                break;
//                
//            case 3:
//                break;
//            
//            default:
//                return result; 
//        }
//    }


    private static bool IsTileCompatibleOO(Tile originalTile,int xPos, int yPos, Tile[,] tileMap)
    {
        if (yPos == 0) return true;
        return AreTilesCompatible(originalTile.OO, tileMap[xPos, yPos].UU);
    }

    private static bool IsTileCompatibleOL(Tile originalTile,int xPos, int yPos, Tile[,] tileMap)
    {
        // Gerade Positionen
        if (xPos % 2 == 0)
        {
            if (xPos == 0) return true;
            return AreTilesCompatible(originalTile.OL, tileMap[xPos - 1, yPos].UR);
        }
        else
        {
            if (yPos == 0) return true;
            return AreTilesCompatible(originalTile.OL, tileMap[xPos - 1, yPos-1].UR);
        }
    }
    
    private static bool IsTileCompatibleOR(Tile originalTile,int xPos, int yPos, Tile[,] tileMap, int width)
    {
        if (xPos == width) return true;
        // Gerade Positionen
        if (xPos % 2 == 0)
        {
            return true;
        }
        else
        {
            if (yPos == 0) return true;
            return AreTilesCompatible(originalTile.OL, tileMap[xPos + 1, yPos-1].UR);
        }        
    }    
    
    private static bool AreTilesCompatible(int tilepos_a, int tilepos_b)
    {
        if (tilepos_a != Land) return true;
        if (tilepos_a == tilepos_b) return true;
        return false;
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
    public string Name { get; set; }
    
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
}

}