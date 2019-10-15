using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrol : MonoBehaviour
{
    public Transform EndLine;
    public Transform FloorLine;
    bool TouchingWall;
    bool TouchingFloor;
    public float PatrolSpeed;
    Vector2 PatrolDirection = new Vector2(1, 0);
    public bool avoidsLedges;

    private void Update()
    {
        RaycastingWall();
        RaycastingFloor();
        WalkAround();
        if (TouchingWall)
        {
            FlipCharacter();
            PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
        }
        if (avoidsLedges && !TouchingFloor)
        {
            FlipCharacter();
            PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
        }
    }

    void RaycastingWall()
    {
        Debug.DrawLine(transform.position, EndLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingWall = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile

    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, FloorLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingFloor = Physics2D.Linecast(transform.position, FloorLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile

    }

    public void WalkAround()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(PatrolDirection * PatrolSpeed);
        gameObject.GetComponent<Rigidbody2D>().velocity = PatrolDirection * PatrolSpeed;
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
    }
}
