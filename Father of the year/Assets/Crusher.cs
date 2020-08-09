using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{

    public bool Horizontal;
    public bool Vertical;
    public float MoveSpeed;
    bool moving;
    Transform Player;
    bool SightBlocked;



    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (SightBlocked == false)
            {
                if (Horizontal)
                {
                    if (gameObject.GetComponentInParent<Rigidbody2D>().velocity.y == 0 && gameObject.GetComponentInParent<Rigidbody2D>().velocity.x == 0)
                    {
                        moving = true;
                        if (transform.parent.position.x < collision.transform.position.x) // Im left of the player
                        {
                            transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed;
                        }
                        else if (transform.parent.position.x > collision.transform.position.x) // im to the right of the player
                        {
                            transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed;
                        }
                    }

                }
                else if (Vertical)
                {
                    if (gameObject.GetComponentInParent<Rigidbody2D>().velocity.x == 0 && gameObject.GetComponentInParent<Rigidbody2D>().velocity.y == 0)
                    {
                        moving = true;
                        if (transform.parent.position.y < collision.transform.position.y) // im below the player
                        {
                            transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * MoveSpeed;
                        }
                        else if (transform.parent.position.y > collision.transform.position.y) // im above the player
                        {
                            transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.down * MoveSpeed;
                        }
                    }

                }
            }
            
        }
    }
    private void Update()
    {
        Raycasting();
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.x == 0 || transform.parent.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            moving = false;
        }

        if (moving == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(transform.parent.position, Player.position, Color.green);
        SightBlocked = Physics2D.Linecast(transform.parent.position, Player.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
