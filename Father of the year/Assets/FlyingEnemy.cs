using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    Transform Player;
    public float FlySpeed;
    public float ReturnSpeed;
    Vector2 MoveDirection;
    float xMove;
    float yMove;
    public bool SightBlocked;
    public bool Disturbed;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        xMove = (Player.position.x - gameObject.transform.position.x);
        yMove = (Player.position.y - gameObject.transform.position.y);
        MoveDirection = new Vector2(xMove, yMove);
        Raycasting();


        // flips sprite based on relation to player
        if (xMove < 0 && Disturbed) // left facing
        {
            transform.parent.localScale = new Vector2(-Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
        }
        if (xMove > 0 && Disturbed) // right facing
        {
            transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // While in range, add a force towards the player
    {
        if (collision.tag == "Player" && !SightBlocked)
        {
            GetComponentInParent<Animator>().SetBool("Attacking", true);
            Disturbed = true;
            GetComponentInParent<Rigidbody2D>().AddForce(MoveDirection * FlySpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // Player escaped range, add a comeback force
    {
        if (collision.tag == "Player")
        {
            if (Disturbed)
            {
                GetComponentInParent<Rigidbody2D>().AddForce(MoveDirection * FlySpeed * ReturnSpeed);
            }
        }
    }

    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(transform.position, Player.position, Color.green);
        SightBlocked = Physics2D.Linecast(transform.position, Player.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
