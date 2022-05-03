using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playermovement;
    [HideInInspector] public PlayerHealth playerhealth;
    [HideInInspector] public ShooterScript shooterscript;
    private void Start()
    {
        playermovement = FindObjectOfType<PlayerMovement>();
        playerhealth = FindObjectOfType<PlayerHealth>();
        shooterscript = FindObjectOfType<ShooterScript>();
    }
    
}
