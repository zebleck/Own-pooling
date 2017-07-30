using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    
    public Camera Camera;

    TileMap TileMap;
    Rect View {
        get {
            int height = (int) (2f * Camera.orthographicSize);
            int width = (int) (height * Camera.aspect);

            return new Rect((int) (Camera.transform.position.x - width / 2f) -2, 
                (int) (Camera.transform.position.y - height / 2f) -2, 
                width +4, 
                height +4);
        }
    }
    HashSet<Tile> LoadedTiles;
    Pool<RenderTile> RenderTilePool;
    HashSet<Tile> InCurrentView;
    HashSet<Tile> ToRemove;

    void OnEnable()
    {
        Circumnav Circumnav = FindObjectOfType<Circumnav>();
        Circumnav.OnLeaveLeft += OnLeaveLeft;
        Circumnav.OnLeaveRight += OnLeaveRight;
    }

    void OnDisable()
    {
        Circumnav Circumnav = FindObjectOfType<Circumnav>();
        Circumnav.OnLeaveLeft -= OnLeaveLeft;
        Circumnav.OnLeaveRight -= OnLeaveRight;
    }

    // Use this for initialization
    void Start () {
        TileMap = GameObject.FindGameObjectWithTag("Current Map").GetComponent<TileMap>();
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

        int ModX;

        for(int y = starty; y < endy; y++)
        {
            for(int x = startx; x < endx; x++)
            {
                ModX = MathHelper.Mod(x, TileMap.width);

                Tile tile = TileMap.getTile(ModX, y);
                if (tile != null)
                {
                    InCurrentView.Add(tile);

                    if (!LoadedTiles.Contains(tile))
                    {

                        RenderTile RenderTile = RenderTilePool.Obtain();

                        if (y >= 5)
                        {
                            RenderTile.Set(new Vector2(x, y), "", new Color(0.25f, 0.25f, 0.75f, 1f), false, "Background");
                        } else
                        {
                            Color color = new Color(0.25f, ((ModX + (y % 2)) % 2) * 0.75f, 0.25f, 1f);
                            RenderTile.Set(new Vector2(x, y), "", color, true, "Solid");
                        }
                            
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

    void OnLeaveLeft()
    {
        foreach(Tile tile in LoadedTiles) {
            RenderTile RenderTile = tile.RenderTile;

            RenderTile.transform.position = new Vector3(RenderTile.transform.position.x + TileMap.width, RenderTile.transform.position.y, RenderTile.transform.position.z);
        }
        Refresh();
    }

    void OnLeaveRight()
    {
        foreach (Tile tile in LoadedTiles)
        {
            RenderTile RenderTile = tile.RenderTile;

            RenderTile.transform.position = new Vector3(RenderTile.transform.position.x - TileMap.width, RenderTile.transform.position.y, RenderTile.transform.position.z);
        }
        Refresh();
    }
}
