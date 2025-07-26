using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    Animator animator;
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AdjustPlayerFacingDirection();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        animator.SetFloat("xvelocity", movement.x);
        animator.SetFloat("yvelocity", movement.y);
    }
    private void AdjustPlayerFacingDirection()
    {

        if (movement.x > 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
    }
}
