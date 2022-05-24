using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float redFlashSeconds;
    public float invincibilityTime;
    public bool invincible = false;
    public float slowMotionTimeScale = 0.5f;
    public Animator cameraAnimator;
    public SpriteRenderer[] playerSprites;

    public UnityEvent<int> OnPlayerDamaged;

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
            StartCoroutine(Attacked(redFlashSeconds));
            StartCoroutine(slowMotion(0.6f)); //TODO: Not hardcode man :/
            OnPlayerDamaged.Invoke((int)damage);
        }
    }

    IEnumerator InvincibilityTime(float seconds)
    {
        invincible = true;
        yield return new WaitForSeconds(seconds);
        invincible = false;
    }

    IEnumerator Attacked(float seconds)
    {
        cameraAnimator.SetTrigger("CameraShake");

        //The following makes the player flash red for a short while and also slows down time.
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

    IEnumerator slowMotion(float seconds)
    {
        Time.timeScale = slowMotionTimeScale;
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 1;

    }
}
