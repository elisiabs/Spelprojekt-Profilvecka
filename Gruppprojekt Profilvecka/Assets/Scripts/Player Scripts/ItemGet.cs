using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GameManager.Instance.player.bodyAnimator;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag.Contains("Player") && !col.isTrigger)
        {
            col.gameObject.GetComponentInChildren<ShooterScript>().Shooter1Unlocked = true;
            playerAnimator.SetTrigger("Item");
            Debug.Log("Gun unlocked trigger");
            Destroy(gameObject);
            
        }
    }
}
