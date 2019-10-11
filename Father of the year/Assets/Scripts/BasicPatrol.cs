using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrol : MonoBehaviour
{
    public Transform EndLine;
    bool TouchingWall;
    public float PatrolSpeed;
    Vector2 PatrolDirection = new Vector2(1, 0);
    bool TouchingPlayer;

    private void Update()
    {
        if (PlayerHealth.Dead == false) // stop if player is dead
        {
            Raycasting();
            WalkAround();
            if (TouchingWall)
            {
                FlipCharacter();
                PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
            }
            if (TouchingPlayer)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
        }

    }

    void Raycasting()
    {
        Debug.DrawLine(transform.position, EndLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingWall = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile

        Debug.DrawLine(transform.position, EndLine.position, Color.green); 
        TouchingPlayer = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void WalkAround()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(PatrolDirection * PatrolSpeed);
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
    }
}
