using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destruktable : MonoBehaviour
{
    public Transform DoorPiece1;
    public Transform DoorPiece2;
    public Transform DoorPiece3;
    public Transform DoorPiece4;

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
                DoorPiece2.parent = null;
                DoorPiece3.parent = null;
                DoorPiece4.parent = null;
            }
            Destroy(gameObject);
        }
    }
}
