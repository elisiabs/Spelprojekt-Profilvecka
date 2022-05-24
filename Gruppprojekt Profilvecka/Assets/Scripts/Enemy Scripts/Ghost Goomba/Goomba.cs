using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
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
    public GameObject groundCheck;

    public float enemyWidth;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGrounded();
    }


    public void Movement()
    {
        if (movingLeft)
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
        Vector3 offsetPosition = transform.position + new Vector3(enemyWidth, 0, 0) * movingDirection;

        RaycastHit2D hitground = Physics2D.Raycast(offsetPosition, Vector2.down, groundDistance, ground);

        if(hitground.transform == null)
        {
            movingDirection *= -1;

            if(movingDirection > 0)
            {
                movingLeft = false;
            }
            else if(movingDirection < 0)
            {
                movingLeft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Damage player.
        }
    }

    public float width;
    public float movingDirection = 1;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f);

        Gizmos.color = Color.red;

        Vector3 widthObject = new Vector3(enemyWidth, 0, 0);

        Vector3 offsetPosition = transform.position + widthObject * movingDirection;
        Gizmos.DrawWireSphere(offsetPosition, 0.1f);
        Gizmos.DrawRay(offsetPosition, Vector3.down);
    }

}

