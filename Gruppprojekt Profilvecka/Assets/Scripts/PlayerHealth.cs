using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float redFlashSeconds;
    public float invincibilityTime;
    public bool invincible = false;
    public SpriteRenderer[] playerSprites;
    public Image[] hearts;
    public Sprite brokenHeart;

    void Start()
    {
        
    }
    void Update()
    {

    }
    public void damagePlayer(float damage)
    {
        if (!invincible)
        {
            health -= damage;
            StartCoroutine(InvincibilityTime(invincibilityTime));
            StartCoroutine(RedFlash(redFlashSeconds));
        }

        switch (health)
        {
            case 0:
                {
                    //TODO: implement die
                    Debug.Log("You died.");

                    //hearts[0].color = new Color(0, 0, 0, 0);
                    hearts[0].sprite = brokenHeart;
                    break;
                }
            case 1:
                {
                    //hearts[1].color = new Color(0, 0, 0, 0);
                    hearts[1].sprite = brokenHeart;
                    break;
                }
            case 2:
                {
                    //hearts[2].color = new Color(0,0,0,0);
                    hearts[2].sprite = brokenHeart;
                    break;
                }
        }
    }

    IEnumerator InvincibilityTime(float seconds)
    {
        invincible = true;
        yield return new WaitForSeconds(seconds);
        invincible = false;
    }

    IEnumerator RedFlash(float seconds)
    {
        for (int i = 0; i < playerSprites.Length; i++)
        {
            playerSprites[i].color = Color.red;
        }
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < playerSprites.Length; i++)
        {
            playerSprites[i].color = Color.white;
        }
    }
}
