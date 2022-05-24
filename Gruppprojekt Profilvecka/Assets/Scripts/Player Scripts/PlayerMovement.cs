using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    [SerializeField] private Collider2D feetCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer leftLegSprite;
    [SerializeField] private SpriteRenderer rightLegSprite;
    [SerializeField] private GameObject parametricLight;

    [Header("Variables")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float moveSmooth;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteTime;
    [Space]
    [SerializeField] private bool canJump;
    [SerializeField] private float preventMovementFalloff = 1f;
    [SerializeField] private float preventGravityFalloff = 0.5f;

    //Variables that do not need to be shown in the inspector.
    private float move;
    private float previousKnockback;
    public Vector2 knockbackForce = Vector2.zero;
    bool jumpPressed = false;
    float jumpLastPressed = 0f;

    Vector2 m_Velocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        
            Jumpable();
            Walk();
            Jump();
        
    }

    private void Walk()
    {
        move = 0;

        Vector3 targetVelocity = new Vector2(move * movementSpeed, rb.velocity.y - previousKnockback);

        if (Input.GetAxisRaw("Horizontal") > 0) //Walk right
        {
            move = 10;
            animator.SetBool("Walk", true);
            bodySprite.flipX = false;
            leftLegSprite.flipX = false;
            rightLegSprite.flipX = false;

            parametricLight.transform.localPosition = new Vector2(0.128f, 0.508f);
            //Jaja jag vet att detta ovan är hardcoded men vill du verkligen se ännu en i princip värdelös variabel?
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) //Walk left
        {
            move = -10;
            animator.SetBool("Walk", true);
            bodySprite.flipX = true;
            leftLegSprite.flipX = true;
            rightLegSprite.flipX = true;

            parametricLight.transform.localPosition = new Vector2(-0.128f, 0.508f);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        float gravity = rb.velocity.y;
        if (knockbackForce.magnitude > preventGravityFalloff)
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
        bool jumpInput = Input.GetButtonDown("Jump");
        bool jumpBuffer = jumpPressed && jumpLastPressed + jumpBufferTime > Time.time;

        if(jumpPressed)
        {
            //Debug.Log("Buffer " + (jumpLastPressed + jumpBufferTime));
            //Debug.Log("Time " + Time.time);
        }

        if (jumpInput == true || jumpBuffer)
        {
            if (canJump)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpHeight);
                canJump = false;
                jumpPressed = false;
            }
            else if(!jumpBuffer)
            {
                jumpPressed = true;
                jumpLastPressed = Time.time;
            }
        }
    }

    private void Jumpable()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
        List<Collider2D> hitColliders = new List<Collider2D>();
        int amountHit = feetCollider.OverlapCollider(contactFilter, hitColliders);
        bool foundPlatform = false;
        if (amountHit > 0)
        {
            for (int i = 0; i < hitColliders.Count; i++)
            {
                if (hitColliders[i].CompareTag("Platform") == true)
                {
                    canJump = true;
                    foundPlatform = true;
                }
                else if(hitColliders[i].gameObject.layer == 4) //4 = water
                {
                    canJump = true;
                    foundPlatform = true;
                }
            }
            if(foundPlatform == false)
            {
                StartCoroutine(CoyoteTime(coyoteTime));
            }
        }
    }

    IEnumerator CoyoteTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canJump = false;
    }
}
