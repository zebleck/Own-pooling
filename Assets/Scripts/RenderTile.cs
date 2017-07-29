using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTile : IPoolable {

    GameObject GO;
    TextMesh TextMesh;

    public RenderTile()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        GO = GameObject.Instantiate(Resources.Load<GameObject>("Tile"));
        TextMesh = GO.GetComponentInChildren<TextMesh>();
    }

    public void Set(Vector2 position, String text)
    {
        GO.SetActive(true);
        GO.transform.position = new Vector3(position.x, position.y, GO.transform.position.z);
        TextMesh.text = text;
    }

    public void Reset()
    {
        GO.transform.position = new Vector3(0, 0, GO.transform.position.z);
        GO.SetActive(false);
        TextMesh.text = "";
    }
}
