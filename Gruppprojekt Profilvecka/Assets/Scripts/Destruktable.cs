using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destruktable : MonoBehaviour
{
    public Tilemap DestruktableTileMap;
    public Transform DoorPiece1;
    public bool detachPiece = true;
    private void Start()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("PlayerProjectile"))
        {
            if (detachPiece == true)
            {
                Debug.Log("wut");
                DoorPiece1.parent = null;
            }
            Destroy(gameObject);
        }
    }
}
