using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public ShooterScript shooterScript;
    [HideInInspector] public Animator bodyAnimator;
    [HideInInspector] public Animator shooterAnimator;

    public void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        shooterScript = FindObjectOfType<ShooterScript>();
        bodyAnimator = playerMovement.gameObject.GetComponent<Animator>();
        shooterAnimator = shooterScript.gameObject.GetComponent<Animator>();
    }
}
