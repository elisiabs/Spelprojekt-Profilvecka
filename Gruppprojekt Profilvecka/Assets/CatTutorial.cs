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
            switch (step)
            {
                case (0):
                    {
                        if (transform.position.x >= steps[0])
                        {
                            animator.SetBool("Walk", false);
                            step++;
                            playerWasClose = false;
                        }
                        else
                        {
                            animator.SetBool("Walk", true);
                            float vX = speed * Time.deltaTime;
                            float vY = transform.position.y;
                            transform.position = new Vector2(transform.position.x + vX, vY);
                        }
                        break;
                    }
                case (1):
                    {
                        if (transform.position.x >= steps[1])
                        {
                            step++;
                            playerWasClose = false;
                        }
                        else
                        {
                            animator.SetTrigger("Jump");
                        }
                        break;
                    }
                case (2):
                    {

                        break;
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
