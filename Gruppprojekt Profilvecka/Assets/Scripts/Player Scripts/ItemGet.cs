using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    [SerializeField][Tooltip("Current possible inputs are: 'Shooter1' or 'Shooter2'.")]
    private string whichShooter;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GameManager.Instance.player.bodyAnimator;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Player") && !col.isTrigger)
        {
            GameManager.Instance.player.shooterScript.UnlockWeapon(whichShooter);
            playerAnimator.SetTrigger("Item");
            //Debug.Log("Gun unlocked trigger");
            Destroy(gameObject);
        }
    }
}
