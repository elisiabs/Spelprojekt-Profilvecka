using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public PlayerHealth player;
   
    public float health;
    public float damage;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.damagePlayer(damage);
        }
        else if (col.gameObject.tag == "PlayerProjectile")
        {
            health -= 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.damagePlayer(damage);
        }
        else if (col.gameObject.tag == "PlayerProjectile")
        {
            health -= 1;
        }
    }
}
