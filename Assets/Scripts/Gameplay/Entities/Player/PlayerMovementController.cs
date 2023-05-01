using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OTBG.Utility;
using Sirenix.OdinInspector;

public enum PlayerState
{
    Normal,
    InRoll,
    Stunned
}

public class PlayerMovementController : MonoSingleton<PlayerMovementController>
{
    [FoldoutGroup("References"), SerializeField] private Animator animator;
    [FoldoutGroup("References"), SerializeField] private Rigidbody2D rigidBody;
    [FoldoutGroup("References"), SerializeField] private PlayerAttack playerAttack;

    [FoldoutGroup("Movement")] public PlayerState currentState;
    [FoldoutGroup("Movement"),SerializeField] private float moveSpeed;
    [FoldoutGroup("Movement"),SerializeField] private float verticalFactor;
    [FoldoutGroup("Movement"),SerializeField] private float startingRollSpeed;
    [FoldoutGroup("Movement"), SerializeField] private float rollDropOffFactor;

    private float rollSpeed; // represents current speed while rolling, drops off back down to match movespeed   
    private Vector3 moveDir;
    private Vector3 rollDir;

    private bool isFacingRight;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();

        isFacingRight = true;
        currentState = PlayerState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PlayerState.InRoll:
                InRoll();
                break;
            case PlayerState.Normal:
                Normal();
                break;
        }
        
    }

    private void Stunned()
    {
        
    }

    private void InRoll()
    {
        // ignore inputs while rolling, just handle speed
        rollSpeed -= rollSpeed * rollDropOffFactor * Time.deltaTime;
        float rollSpeedMinimum = moveSpeed;
        if (rollSpeed < rollSpeedMinimum)
        {
            currentState = PlayerState.Normal;
        }
    }

    private void Normal()
    {
        if (!CanMove())
            return;
        //These will get both the WASD and Arrow keys respectively, Better than giant If statement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(moveX, moveY).normalized;
        moveDir.y *= verticalFactor;

        if(animator != null)
            animator.SetFloat("speed", Mathf.Abs(moveDir.magnitude));

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetTrigger("roll");
            rollSpeed = startingRollSpeed;
            rollDir = moveDir;
            currentState = PlayerState.InRoll;
            // pause walk wile roll/dash
            // animator.SetFloat("speed", 0.0f);
        }

        if (moveX > 0) isFacingRight = true;
        else if (moveX < 0) isFacingRight = false;

        Flip();
    }

    public bool CanMove()
    {
        //Could you implement something like this in the attackController. This way we can cleanly organise it :)
        //if (playerAttack.isPlayerAttacking) return false;
        return true;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Normal:
                rigidBody.velocity = moveDir * moveSpeed;
                // animator.SetFloat("speed", rigidBody.velocity.sqrMagnitude);
                if(rigidBody != null)
                    rigidBody.velocity = moveDir * moveSpeed;
                break;
            case PlayerState.InRoll:
                if(rigidBody != null)
                    rigidBody.velocity = rollDir *rollSpeed;
                break;
        }
    }

    //Tidied it up so the flip is constantly running, it's not going to impact performance.
    //Saves on having to write more code above in the Normal Function
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x = isFacingRight ? 1 : -1;
        transform.localScale = theScale;
    }
}
