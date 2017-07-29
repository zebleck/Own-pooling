using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Everything : MonoBehaviour {

    public int width;
    public int height;
    public GameObject ViewObject;

    TileMap TileMap;
    Rect View { get { return new Rect(ViewObject.transform.position.x - ViewObject.transform.localScale.x / 2f, ViewObject.transform.position.y - ViewObject.transform.localScale.y / 2f, ViewObject.transform.localScale.x, ViewObject.transform.localScale.y); } }
    HashSet<Tile> LoadedTiles;
    Pool<RenderTile> RenderTilePool;
    HashSet<Tile> InCurrentView;
    HashSet<Tile> ToRemove;

    // Use this for initialization
    void Start () {
        TileMap = new TileMap(width, height);
        LoadedTiles = new HashSet<Tile>();
        RenderTilePool = new Pool<RenderTile>();
        InCurrentView = new HashSet<Tile>();
        ToRemove = new HashSet<Tile>();
    }
	
	// Update is called once per frame
	void Update () {
      Refresh();
	}

    private void Refresh()
    {
        int startx, starty, endx, endy;

        startx = (int)(View.position.x);
        starty = (int)(View.position.y);

        endx = (int)(View.position.x + View.width);
        endy = (int)(View.position.y + View.height);

        InCurrentView.Clear();
        ToRemove.Clear();

        for(int y = starty; y < endy; y++)
        {
            for(int x = startx; x < endx; x++)
            {
                Tile tile = TileMap.getTile(x, y);
                if (tile != null)
                {
                    InCurrentView.Add(tile);

                    if (!LoadedTiles.Contains(tile))
                    {

                        RenderTile RenderTile = RenderTilePool.Obtain();
                        RenderTile.Set(new Vector2(x, y), tile.n.ToString());

                        tile.RenderTile = RenderTile;

                        LoadedTiles.Add(tile);
                    }
                }
            }
        }

        // Add tiles to be removed if they were not in View
        foreach (Tile tile in LoadedTiles)
        {
            if(!InCurrentView.Contains(tile))
            {
                ToRemove.Add(tile);
            }
        }

        // Remove tiles to be removed
        foreach (Tile tile in ToRemove)
        {
            RenderTilePool.Free(tile.RenderTile);
            tile.RenderTile = null;
            LoadedTiles.Remove(tile); 
        }
    }
}
