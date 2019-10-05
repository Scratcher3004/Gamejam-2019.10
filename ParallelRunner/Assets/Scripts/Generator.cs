using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Generator
{

    // 0 = OL, 1 = OR, 2 = R, 3 = UR, 4 = UL, 5 = L
    
    public static Tile Tile0;
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
        Tile0 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile1 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile2 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile3 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile4 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile5 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile6 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile7 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0}; 
        Tile8 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0};
        Tile9 = new Tile{OO = 0, OL = 2, OR = 2, UL = 0, UR = 2, UU = 0};  
    }


    public static Tile[,] GetTileMap(int width, int height)
    {
        Tile[,] result = new Tile[width,height];
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                result[x, y] = Tile0;
            }
        }
        
        return result;
    }
    
    
    public class Tile {

        
        // 0 = Land, 1 = Block, 2= Road, 3= Water
        public int OO {get; set;}
        public int OL {get; set;}        
        public int UL {get; set;}
        public int OR {get; set;}
        public int UR {get; set;}
        public int UU {get; set;}
    }    

    

}
