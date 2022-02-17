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
        if(col.gameObject.tag == "Player")
        {
            player.damagePlayer(damage);
        }
        else if(col.gameObject.layer == 7)//7 is "PlayerProjectile".
        {
            health -= 1;
        }
    }
}
