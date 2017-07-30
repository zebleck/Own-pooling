using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circumnav : MonoBehaviour {

    public delegate void LeaveEdgeEvent();
    public event LeaveEdgeEvent OnLeaveLeft, OnLeaveRight;

    

    TileMap TileMap;

    // Use this for initialization
    void Start () {
        if (TileMap == null)
            TileMap = GameObject.FindWithTag("Current Map").GetComponent<TileMap>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(transform.position.x + TileMap.width, transform.position.y, transform.position.z);
            if(OnLeaveLeft != null)
                OnLeaveLeft();
        } else if(transform.position.x > TileMap.width)
        {
            transform.position = new Vector3(transform.position.x - TileMap.width, transform.position.y, transform.position.z);
            if (OnLeaveRight != null)
                OnLeaveRight();
        }
	}
}
