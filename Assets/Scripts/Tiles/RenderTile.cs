using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTile : IPoolable {

    public Transform transform;
    GameObject GO;
    TextMesh TextMesh;
    SpriteRenderer SpriteRenderer;
    BoxCollider2D BC2D;
    PlatformEffector2D PE2D;

    public RenderTile()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        GO = GameObject.Instantiate(Resources.Load<GameObject>("Tile"));
        transform = GO.transform;
        TextMesh = GO.GetComponentInChildren<TextMesh>();
        SpriteRenderer = GO.GetComponent<SpriteRenderer>();
        BC2D = GO.GetComponent<BoxCollider2D>();
        PE2D = GO.GetComponent<PlatformEffector2D>();
    }

    public void Set(Vector2 position, String text, Color color, bool isSolid, String layer)
    {
        GO.SetActive(true);
        GO.transform.position = new Vector3(position.x, position.y, GO.transform.position.z);
        TextMesh.text = text;
        SpriteRenderer.color = color;
        BC2D.enabled = isSolid;
        PE2D.enabled = isSolid;
        GO.layer = LayerMask.NameToLayer(layer);

    }

    public void Reset()
    {
        GO.SetActive(false);
    }
}
