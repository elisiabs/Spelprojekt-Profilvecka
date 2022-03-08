using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destruktable : MonoBehaviour
{
    public Tilemap DestruktableTileMap;

    private void Start()
    {
        DestruktableTileMap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Vector3 hitPosision = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosision.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosision.y = hit.point.y - 0.01f * hit.normal.y;
                DestruktableTileMap.SetTile(DestruktableTileMap.WorldToCell(hitPosision), null);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Vector3 hitPosision = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosision.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosision.y = hit.point.y - 0.01f * hit.normal.y;
                DestruktableTileMap.SetTile(DestruktableTileMap.WorldToCell(hitPosision), null);
            }
        }
    }
}
