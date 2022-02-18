using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public PlayerHealth player;
    public SpriteRenderer sprite;
   
    public float health;
    public float damage;
    public float redFlashSeconds;

    private void Update()
    {
        
    }

    private void damageEnemy(int damage)
    {
        health -= damage;
        StartCoroutine(RedFlash(redFlashSeconds));
    }

    IEnumerator RedFlash(float seconds)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(seconds);
        sprite.color = Color.white;
        if (health <= 0) //The reason this is here is because I want the red flash to finish before enemy dies.
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
