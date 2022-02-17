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
        //GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
        //transform.DetachChildren();
        //FIX THIS. The above code was supposed to fix the bug with bullet trails disappearing too quickly
        //but does not. I do not have the patience to fix this now so someone else, or more likely future
        //me has gotta fix it. Thanks and bye. -Elias
        Destroy(gameObject);
    }

    IEnumerator DestroyBulletAfterTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
