using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap {

    public int width;
    public int height;
    Tile[,] Tiles;

    // Use this for initialization
    public TileMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        Tiles = new Tile[width, height];
        generate();
    }

    public void generate()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Tiles[x, y] = new Tile(y * width + x);
                //GameObject GO = GameObject.Instantiate(Resources.Load<GameObject>("Tile"));
                //GO.transform.position = new Vector2(x-width/2, y-height/2);
                //GO.GetComponentInChildren<TextMesh>().text = (width * y + x).ToString();
                //GO.SetActive(false);
            }
        }
    }

    public Tile getTile(int x, int y) {
        return (x >= 0 && x < width && y >= 0 && y < height) ? Tiles[x, y] : null;
    }


}
