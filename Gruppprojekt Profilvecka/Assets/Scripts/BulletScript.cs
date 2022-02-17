using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletDespawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBulletAfterTime(bulletDespawnTime));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyBulletAfterTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
