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
                rigidbody.AddForce(collision.transform.right * 10, ForceMode2D.Impulse);

                BoxCollider2D boxCollider = DoorPieces[i].GetComponent<BoxCollider2D>();
                boxCollider.size = boxCollider.size * 0.8f;

                DoorPieces[i].parent = null;

                Destroy(DoorPieces[i].gameObject, 2f);
            }
            Destroy(gameObject);
        }
    }
}
