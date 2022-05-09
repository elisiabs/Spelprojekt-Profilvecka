using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public bool movingLeft;
    private Rigidbody2D rb2d;


    // movement checks
    public GameObject leftCheck;
    public GameObject rightCheck;
    public float groundDistance;
    public float wallDistance;
    public LayerMask ground;
    public bool leftGrounded;
    public bool rightGrounded;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGrounded();
    }


    public void Movement()
    {
        if(movingLeft)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
    }

    public void CheckGrounded()
    {
        RaycastHit2D hitleft = Physics2D.Raycast(leftCheck.transform.position, Vector2.down, groundDistance, ground);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(leftCheck.transform.position, Vector2.left, wallDistance, ground);

        Debug.Log("jj");
        Debug.DrawLine(leftCheck.transform.position, leftCheck.transform.position + Vector3.left * wallDistance, Color.red);
        Debug.DrawLine(leftCheck.transform.position, leftCheck.transform.position + Vector3.down * groundDistance, Color.cyan);

        if (movingLeft && hitWallLeft.transform !=null)
        {
            movingLeft = false;
        }

        if (hitleft.transform != null)
        {
            leftGrounded = true;
        }

        else
        {
            leftGrounded = false;
            movingLeft = false;
        }


        RaycastHit2D hitRight = Physics2D.Raycast(rightCheck.transform.position, Vector2.down, groundDistance, ground);
        RaycastHit2D hitWallRight = Physics2D.Raycast(rightCheck.transform.position, Vector2.right, wallDistance, ground);

        Debug.DrawLine(rightCheck.transform.position, rightCheck.transform.position + Vector3.right * wallDistance, Color.red);
        Debug.DrawLine(rightCheck.transform.position, rightCheck.transform.position + Vector3.down * groundDistance, Color.cyan);

        if (hitRight.transform != null)
        {
            rightGrounded = true;
        }

        else
        {
            rightGrounded = false;
            movingLeft = true;
        }

        if(hitWallLeft.transform != null)
        {
            movingLeft = false;
        }
        if (hitWallRight.transform != null)
        {
            movingLeft = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.playerHealth.damagePlayer(1);
        }
    }



}
