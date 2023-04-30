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
        float dX = enemyRef.moveDest.x - transform.position.x > 0 ? moveSpeed : -1 * moveSpeed;
        float dY = enemyRef.moveDest.y - transform.position.y > 0 ? moveSpeed : -1 * moveSpeed;

        // TODO: Should players and enemies share a y speed factor?
        targetVelocity = new Vector2(dX, dY * 0.75f);

        // If the input is moving the player right and the player is facing left...
        if (dX > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the enemy is moving left but facing right...
        else if (dX < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = targetVelocity;
    }

    public void StopMovement()
    {
        targetVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (enemyRef !=null && enemyRef.moveDest != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(enemyRef.moveDest, 0.25f);
            Handles.Label(new Vector3(enemyRef.moveDest.x, enemyRef.moveDest.y - 0.5f), "Move Dest");
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
