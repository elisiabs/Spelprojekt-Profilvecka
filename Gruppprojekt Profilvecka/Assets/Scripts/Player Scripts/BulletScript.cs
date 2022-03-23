using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletDespawnTime;
    public Transform trail;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBulletAfterTime(bulletDespawnTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        trail.SetParent(null);
        Destroy(gameObject);
    }

    IEnumerator DestroyBulletAfterTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
