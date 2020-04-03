using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrol : MonoBehaviour
{
    public Transform EndLine;
    public Transform FloorLine;
    public Transform EnemyStart;
    public Transform EnemyLine;
    bool TouchingWall;
    public bool TouchingFloor;
    bool TouchingEnemy;
    bool TouchingPlayer;
    bool TouchingObstacle;
    public float PatrolSpeed;
    public Vector2 PatrolDirection = new Vector2(1, 0);
    public bool avoidsLedges;
    public bool flipDirection;
    public bool RedCobra; //for the lunge animation
    public bool attacking;
    public bool Falling;

    private void Awake()
    {
        if (flipDirection)
        {
            FlipCharacter();
            PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
        }
    }

    private void FixedUpdate()
    {
        RaycastingWall();
        RaycastingFloor();
        RaycastingEnemy();
        RaycastingObstacle();
        WalkAround();
        if ((TouchingWall || TouchingEnemy || TouchingObstacle) && !Falling)
        {
            FlipCharacter();
            PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
        }
        if (avoidsLedges && !TouchingFloor && !Falling) // perhaps this is where the cobra spazz glitch occurs.  IT IS.
        {
            FlipCharacter();
            PatrolDirection = new Vector2(PatrolDirection.x * -1, 0);
        }
        if (TouchingFloor == false)
        {
            Falling = true;
        }
        else
        {
            Falling = false;
        }
        if (TouchingFloor && Falling)
        {
            Falling = false;
        }
    }

    void RaycastingWall()
    {
        //Debug.DrawLine(transform.position, EndLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingWall = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile

    }

    void RaycastingEnemy()
    {
        Debug.DrawLine(EnemyStart.position, EnemyLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        TouchingEnemy = Physics2D.Linecast(EnemyStart.position, EnemyLine.position, 1 << LayerMask.NameToLayer("Enemy")); // returns true if line touches a ground tile
    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, FloorLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingFloor = Physics2D.Linecast(transform.position, FloorLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile
    }

    void RaycastingObstacle()
    {
        //Debug.DrawLine(EnemyStart.position, EnemyLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        TouchingObstacle = Physics2D.Linecast(EnemyStart.position, EnemyLine.position, 1 << LayerMask.NameToLayer("Obstacle")); // returns true if line touches an obstacle
    }

    public void WalkAround()
    {
        if (!attacking)
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(PatrolDirection * PatrolSpeed);
            gameObject.GetComponent<Rigidbody2D>().velocity = PatrolDirection * PatrolSpeed;
        }
    }

    public void FlipCharacter()
    {
        if (Falling == false)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && TouchingFloor == true)
        {
            if (gameObject.tag != "Sleeper")
            {
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
            gameObject.GetComponent<Rigidbody2D>().AddForce(PatrolDirection * 10);
        }
    }

    public void StartAttacking()
    {
        attacking = true;
    }
    public void StopAttacking()
    {
        attacking = false;
    }

}
