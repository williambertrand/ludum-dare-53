using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{

    private Enemy enemyRef;
    private EnemyStateHandler stateMachine;

    private Animator animator;

    private float moveSpeed;
    private Vector3 moveDest;

    private Rigidbody2D rigidBody;
    private Vector3 targetVelocity;

    private bool facingRight;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyRef = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Set up movement relevant fields based on enemy stats
        moveSpeed = enemyRef.stats.moveSpeed;
        facingRight = true;
    }

    public void HandleMoveUpdate()
    {
        float xDirection = enemyRef.moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        float yDirection = enemyRef.moveDest.y - transform.position.y > 0 ? moveSpeed : -1 * moveSpeed;
        
        targetVelocity = new Vector2(xDirection, yDirection * 0.75f);
        animator.SetFloat("speed", targetVelocity.magnitude);
        
        Flip(xDirection);
    }

    public void HandleDirectionCheck()
    {
        if (enemyRef.GetTarget() == null)
            return;
        float xDirection = enemyRef.GetTarget().transform.position.x - transform.position.x > 0 ? 1 : -1;
        Flip(xDirection);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = targetVelocity;
    }

    public void StopMovement()
    {
        animator.SetFloat("speed", 0);
        targetVelocity = Vector3.zero;
    }

    private void Flip(float xDirection)
    {
        if (xDirection > 0)
            facingRight = true;
        else if (xDirection < 0)
            facingRight = false;
        
        Vector3 theScale = transform.localScale;
        theScale.x = facingRight ? 1 : -1;
        transform.localScale = theScale;
    }
}
