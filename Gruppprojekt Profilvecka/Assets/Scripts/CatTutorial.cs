using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTutorial : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private int step = 0;
    [SerializeField] private bool playerWasClose = false;
    [SerializeField] private float speed;
    [SerializeField] private float[] checkPoints;
    private bool waitingForJumpAnim = false;

    //Warning: this cat is quite hardcoded, my excuse is that it is only used once to do only one thing.

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
                        if (transform.position.x >= checkPoints[0])
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
                        if (!waitingForJumpAnim)
                        {
                            waitingForJumpAnim = true;
                            animator.SetTrigger("Jump");
                            /*
                            AnimatorClipInfo[] currentClip;
                            currentClip = animator.GetCurrentAnimatorClipInfo(0);
                            StartCoroutine(waitForJumpAnim(currentClip[0].clip.length));
                            */
                            StartCoroutine(waitForJumpAnim(1.683f));
                        }
                        break;
                    }
                case (2):
                    {
                        if (transform.position.x >= checkPoints[1])
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
                        waitingForJumpAnim = false;
                        break;
                    }
                case (3):
                    {
                        if (!waitingForJumpAnim)
                        {
                            waitingForJumpAnim = true;
                            animator.SetTrigger("Jump");
                            StartCoroutine(waitForJumpAnim(1.683f));
                        }
                        break;
                    }
                case (4):
                    {
                        if (transform.position.x >= checkPoints[2])
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
                        waitingForJumpAnim = false;
                        break;
                    }
            }
        }
    }

    private bool checkPlayerClose()
    {
        if (transform.position.x - player.position.x <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator waitForJumpAnim(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.position = new Vector2(transform.position.x + 6.5f, transform.position.y);
        step++;
        playerWasClose = false;
    }
}
