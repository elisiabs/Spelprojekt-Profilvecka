using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playermovement = FindObjectOfType<PlayerMovement>();
    [HideInInspector] public PlayerHealth playerhealth = FindObjectOfType<PlayerHealth>();
    [HideInInspector] public ShooterScript shooterscript = FindObjectOfType<ShooterScript>();
}
