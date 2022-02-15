using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float invincibilityTime;
    public bool invincible = false;

    void Start()
    {
        
    }
    void Update()
    {
        if(health >= 0)
        {
            //die
        }
    }
    public void damagePlayer(float damage)
    {
        if (!invincible)
        {
            health -= damage;
            StartCoroutine(InvincibilityTime(invincibilityTime));
        }
    }

    IEnumerator InvincibilityTime(float seconds)
    {
        invincible = true;
        yield return new WaitForSeconds(seconds);
        invincible = false;
    }
}