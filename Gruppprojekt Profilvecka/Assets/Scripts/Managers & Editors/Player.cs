using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public ShooterScript shooterScript;
    [HideInInspector] public Animator bodyAnimator;
    [HideInInspector] public Animator shooterAnimator;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        shooterScript = FindObjectOfType<ShooterScript>();
        bodyAnimator = playerMovement.gameObject.GetComponent<Animator>();
        shooterAnimator = shooterScript.gameObject.GetComponent<Animator>();
    }
    
}
