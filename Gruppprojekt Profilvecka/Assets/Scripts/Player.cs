using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playermovement;
    [HideInInspector] public PlayerHealth playerhealth;
    [HideInInspector] public ShooterScript shooterscript;
    [HideInInspector] public Animator bodyAnimator;
    [HideInInspector] public Animator shooterAnimator;

    private void Awake()
    {
        playermovement = FindObjectOfType<PlayerMovement>();
        playerhealth = FindObjectOfType<PlayerHealth>();
        shooterscript = FindObjectOfType<ShooterScript>();
        bodyAnimator = playermovement.gameObject.GetComponent<Animator>();
        shooterAnimator = shooterscript.gameObject.GetComponent<Animator>();
    }
    
}
