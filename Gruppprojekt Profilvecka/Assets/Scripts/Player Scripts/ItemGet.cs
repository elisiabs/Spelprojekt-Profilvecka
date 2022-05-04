using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    [SerializeField] private string whichShooter;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GameManager.Instance.player.bodyAnimator;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Player") && !col.isTrigger)
        {
            GameManager.Instance.player.shooterscript.UnlockWeapon(whichShooter);
            playerAnimator.SetTrigger("Item");
            Debug.Log("Gun unlocked trigger");
            Destroy(gameObject);
        }
    }
}
