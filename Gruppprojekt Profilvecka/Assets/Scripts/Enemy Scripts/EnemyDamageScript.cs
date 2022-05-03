using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    public PlayerHealth player;
    public SpriteRenderer sprite;

    [SerializeField] Collider2D physicalCollider;

    public float health;
    public float damage;
    public float redFlashSeconds;

    private void Update()
    {
        
    }

    public void DamageEnemy(int damage)
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
        Debug.Log("Collision");
        if (col.gameObject.tag == "Player")
        {
            player.damagePlayer(damage);
        }
        else if(col.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("träffad.");
            DamageEnemy(1);
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        
    }
    
}
