using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructible : MonoBehaviour
{
    public Transform[] Pieces = new Transform[0];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("PlayerProjectile"))
        {
            for (int i = 0; i < 4; i++)
            {
                Rigidbody2D rigidbody = Pieces[i].gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                rigidbody.AddForce(collision.transform.right * 10, ForceMode2D.Impulse);

                BoxCollider2D boxCollider = Pieces[i].GetComponent<BoxCollider2D>();
                boxCollider.size *= 0.8f;

                Pieces[i].parent = null;

                float despawnTime = Random.Range(2f, 2.5f);

                Destroy(Pieces[i].gameObject, despawnTime);
            }
            Destroy(gameObject);
        }
    }
}
