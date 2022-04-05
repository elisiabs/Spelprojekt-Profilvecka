using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destruktable : MonoBehaviour
{
    public Transform[] DoorPieces = new Transform[4];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("PlayerProjectile"))
        {
                for (int i = 0; i < 4; i++)
                {  
                    Rigidbody2D rigidbody = DoorPieces[i].gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                    DoorPieces[i].parent = null;
                }
            
            Destroy(gameObject);
        }
    }
}
