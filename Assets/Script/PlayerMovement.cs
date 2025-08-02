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
    private AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        AM = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            float x = PlayerPrefs.GetFloat("CurrentPosX");
            float y = PlayerPrefs.GetFloat("CurrentPosY");
            float z = PlayerPrefs.GetFloat("CurrentPosZ");
            transform.position = new Vector3(x, y, z);
            PlayerPrefs.DeleteKey("SaveGame");
        }


    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        AdjustPlayerFacingDirection();
        if (movement == Vector2.zero && AM.Walk.isPlaying)
        {
            AM.Walk.Stop();
        }

        if (movement.x != 0 && !AM.Walk.isPlaying || movement.y != 0 && !AM.Walk.isPlaying)
        {
            AM.Walk.Play();
        }
        
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
            //AM.Walk.Play();
        }
        else if (movement.x < 0)
        {
            mySpriteRenderer.flipX = true;
            //AM.Walk.Play();
        }
    }
}
