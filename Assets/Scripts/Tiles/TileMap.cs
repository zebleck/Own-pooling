using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public int width;
    public int height;
    Tile[,] Tiles;

    public void Start()
    {
        Tiles = new Tile[width, height];
        generate();
    }

    public void Update()
    {
        
    }

    public void generate()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Tiles[x, y] = new Tile(y * width + x);
            }
        }
    }

    public Tile getTile(int x, int y) {
        return (x >= 0 && x < width && y >= 0 && y < height) ? Tiles[x, y] : null;
    }


}
