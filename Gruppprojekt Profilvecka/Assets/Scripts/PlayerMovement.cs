using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    
    public int timesJumped;
    
    public float movementSpeed;
    public float jumpHeight;
    public float moveSmooth;
    float move;
    
    Vector3 m_Velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = 0;
        Vector3 targetVelocity = new Vector2(move * movementSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.D))
        {
            move = 10;
            targetVelocity = new Vector2(move * movementSpeed, rb.velocity.y);
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move = -10;
            targetVelocity = new Vector2(move * movementSpeed, rb.velocity.y);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, moveSmooth);


        if (timesJumped < 1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpHeight);
                timesJumped++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            timesJumped = 0;
        }
    }

}
