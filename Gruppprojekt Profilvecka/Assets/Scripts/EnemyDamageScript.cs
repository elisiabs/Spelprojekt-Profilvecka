using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public PlayerHealth player;
    public SpriteRenderer sprite;
   
    public float health;
    public float damage;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void damageEnemy(int damage)
    {
        health -= damage;
        sprite.color = Color.red;
        StartCoroutine(InvincibilityTime(1));
    }

    IEnumerator InvincibilityTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sprite.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.damagePlayer(damage);
        }
        else if (col.gameObject.tag == "PlayerProjectile")
        {
            damageEnemy(1);
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
            damageEnemy(1);
        }
    }
}
