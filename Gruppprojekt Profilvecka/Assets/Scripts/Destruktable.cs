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
            ContactFilter2D contactFilter = new ContactFilter2D();
            List<Collider2D> hitColliders = new List<Collider2D>();
            collision.OverlapCollider(contactFilter, hitColliders);

            for (int i = 0; i < 4; i++)
            {
                Rigidbody2D rigidbody = DoorPieces[i].gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                for (int i2 = 0; i2 < 4; i2++)
                {
                    if (hitColliders[i2].gameObject.tag == "PlayerProjectile")
                    {
                        Vector2 push = (DoorPieces[i].transform.position - hitColliders[i2].transform.position);
                        DoorPieces[i].GetComponent<Rigidbody2D>().velocity = push;
                    }
                }
                
                DoorPieces[i].parent = null;

                Destroy(DoorPieces[i].gameObject, 2f);
            }
            Destroy(gameObject);
        }
    }
}
