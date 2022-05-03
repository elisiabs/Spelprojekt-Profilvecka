using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{
    private PlayerHealth player;
    [SerializeField] private SpriteRenderer sprite;
    private GameManager gameManager;

    [SerializeField] private Collider2D physicalCollider;

    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float redFlashSeconds;

    private void Update()
    {
        gameManager = GameManager.Instance;
        player = gameManager.player.playerhealth;
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
}
