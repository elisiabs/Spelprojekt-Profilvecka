using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float invincibilityTime;
    public bool invincible = false;
    public SpriteRenderer sprite;

    void Start()
    {
        
    }
    void Update()
    {
        if(health <= 0)
        {
            //die
            Debug.Log("You died.");
        }
    }
    public void damagePlayer(float damage)
    {
        if (!invincible)
        {
            health -= damage;
            sprite.color = Color.red;
            StartCoroutine(InvincibilityTime(invincibilityTime));
        }
    }

    IEnumerator InvincibilityTime(float seconds)
    {
        invincible = true;
        yield return new WaitForSeconds(seconds);
        invincible = false;
        sprite.color = Color.white;
    }
}
