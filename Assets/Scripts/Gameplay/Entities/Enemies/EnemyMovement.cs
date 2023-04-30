using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{

    private Enemy enemyRef;
    private EnemyStateHandler stateMachine;

    private float moveSpeed;
    private Vector3 moveDest;

    private Rigidbody2D rigidBody;
    private Vector3 targetVelocity;

    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyRef = GetComponent<Enemy>();
        // Set up movement relevant fields based on enemy stats

        moveSpeed = enemyRef.stats.moveSpeed;

        facingRight = true;
    }

    public void HandleMoveUpdate()
    {
        //Please don't use analogies. I can't read this. What does dX mean. I'm assuming directionX
        //float dX = enemyRef.moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        //float dY = enemyRef.moveDest.y - transform.position.y > 0 ? moveSpeed : -1 * moveSpeed;

        float xDirection = enemyRef.moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        float yDirection = enemyRef.moveDest.y - transform.position.y > 0 ? moveSpeed : -1 * moveSpeed;



        // TODO: Should players and enemies share a y speed factor?
        //For now, no, they should just have an internal variable that controls it. No need for a global one in GameJams
        targetVelocity = new Vector2(xDirection, yDirection * 0.75f);

        // If input is left or right, set facing right.
        if (xDirection > 0)
            facingRight = true;
        else if (xDirection < 0)
            facingRight = false;

        Flip();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = targetVelocity;
    }

    public void StopMovement()
    {
        targetVelocity = Vector3.zero;
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = facingRight ? 1 : -1;
        transform.localScale = theScale;
    }
    private void OnDrawGizmos()
    {
        if (enemyRef != null && enemyRef.moveDest != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(enemyRef.moveDest, 0.25f);
            Handles.Label(new Vector3(enemyRef.moveDest.x, enemyRef.moveDest.y - 0.5f), "Move Dest");
        }
    }
}
