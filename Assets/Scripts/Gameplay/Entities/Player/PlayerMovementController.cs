using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OTBG.Utility;

public enum PlayerState
{
    Normal,
    InRoll,
    Stunned
}

public class PlayerMovementController : MonoBehaviour
{

    public static PlayerMovementController Instance;

    public PlayerState currentState;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float verticalFactor;
    [SerializeField] private float startingRollSpeed;
    private float rollSpeed; // represents current speed while rolling, drops off back down to match movespeed

    [SerializeField]
    private Animator animator;

    private Rigidbody2D rigidBody;
    private Vector3 moveDir;
    private Vector3 rollDir;

    private bool facingRight;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        facingRight = true;
        currentState = PlayerState.Normal;

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PlayerState.InRoll:
                // ignore inputs while rolling, just handle speed
                float rollSpeedDropoff = 3f;
                rollSpeed -= rollSpeed * rollSpeedDropoff * Time.deltaTime;
                float rollSpeedMinimum = moveSpeed;
                if(rollSpeed < rollSpeedMinimum)
                {
                    currentState = PlayerState.Normal;
                }
                break;
            case PlayerState.Normal:
                float moveX = 0f;
                float moveY = 0f;

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    moveY = 1;
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    moveY = -1;
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1;
                }
                moveDir = new Vector3(moveX, moveY).normalized;
                moveDir.y *= verticalFactor;

                animator.SetFloat("speed", Mathf.Abs(moveX));

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    rollSpeed = startingRollSpeed;
                    rollDir = moveDir;
                    currentState = PlayerState.InRoll;
                }

                // If the input is moving the player right and the player is facing left...
                if (moveX > 0 && !facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (moveX < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                break;
        }
        
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Normal:
                rigidBody.velocity = moveDir * moveSpeed;
                break;
            case PlayerState.InRoll:
                rigidBody.velocity = rollDir *rollSpeed;
                break;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
