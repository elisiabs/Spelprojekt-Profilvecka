using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTutorial : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    private int step = 0;
    private bool playerWasClose = false;
    [SerializeField] private float speed;
    [SerializeField] private float[] steps;

    void Start()
    {
        
    }

    void Update()
    {
        if (checkPlayerClose())
        {
            playerWasClose = true;
        }

        if (playerWasClose)
        {
            if(step == 0)
            {
                if(transform.position.x >= steps[0])
                {
                    animator.SetBool("Walk", false);
                }
                else
                {
                    Debug.Log("lol");
                    animator.SetBool("Walk", true);
                    transform.position = transform.position * speed * Time.deltaTime;
                }
            }
        }
    }

    private bool checkPlayerClose()
    {
        if (Mathf.Abs(player.position.x) / Mathf.Abs(transform.position.x) <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
