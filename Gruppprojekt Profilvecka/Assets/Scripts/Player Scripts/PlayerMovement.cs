using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D feetCollider;
    public Animator animator;
    public SpriteRenderer bodySprite;
    public SpriteRenderer leftLegSprite;
    public SpriteRenderer rightLegSprite;
    public bool canJump;
    public bool inMenu = false;
    
    public float movementSpeed;
    public float jumpHeight;
    public float moveSmooth;
    float move;
    float previousKnockback;
    public float preventMovementFalloff = 1f;
    public float preventGravityFalloff = 0.5f;

    public Vector2 knockbackForce = Vector2.zero;
    
    Vector2 m_Velocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (!inMenu)
        {
            Walk();
            Jump();
        }
    }

    private void Walk()
    {
        move = 0;
        
        Vector3 targetVelocity = new Vector2(move * movementSpeed, rb.velocity.y - previousKnockback);

        if (Input.GetKey(KeyCode.D))
        {
            move = 10;
            animator.SetBool("Walk", true);
            bodySprite.flipX = false;
            leftLegSprite.flipX = false;
            rightLegSprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            move = -10;
            animator.SetBool("Walk", true);
            bodySprite.flipX = true;
            leftLegSprite.flipX = true;
            rightLegSprite.flipX = true;
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        float gravity = rb.velocity.y;
        if(knockbackForce.magnitude > preventGravityFalloff)
        {
            gravity = 0;
        }

        bool forbidRightMove = knockbackForce.x > preventMovementFalloff && move > 0;
        bool forbidLeftMove = knockbackForce.x < -preventMovementFalloff && move < 0;

        if (forbidLeftMove || forbidRightMove)
        {
            move = 0;
        }

        targetVelocity = new Vector2(move * movementSpeed, gravity) + knockbackForce;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, moveSmooth);
        knockbackForce = knockbackForce * 0.8f;
    }

    private void Jump()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpHeight);
                canJump = false;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && collision.otherCollider == feetCollider)
        {
            canJump = true;
            StopCoroutine(JumpMargins(0));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && collision.otherCollider == feetCollider)
        {
            StartCoroutine(JumpMargins(0.15f)); //TODO: not hardcode man 
        }
    }


    IEnumerator JumpMargins(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canJump = false;
    }
}
