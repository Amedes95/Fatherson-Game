using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{

    public bool Horizontal;
    public bool Vertical;
    public float MoveSpeed;
    public bool moving;
    Transform Player;
    bool SightBlocked;
    public GameObject DustParticles;
    public static GameObject ParticlesClone;



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
                    if (transform.parent.position.x < collision.transform.position.x) // Im left of the player
                    {
                        transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.right * MoveSpeed;
                    }
                    else if (transform.parent.position.x > collision.transform.position.x) // im to the right of the player
                    {
                        transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.left * MoveSpeed;
                    }

                }
                else if (Vertical)
                {
                    if (transform.parent.position.y < collision.transform.position.y) // im below the player
                    {
                        transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * MoveSpeed;
                    }
                    else if (transform.parent.position.y > collision.transform.position.y) // im above the player
                    {
                        transform.parent.GetComponentInParent<Rigidbody2D>().velocity = Vector2.down * MoveSpeed;
                    }

                }
                gameObject.GetComponentInParent<Animator>().SetBool("Moving", true);
            }

        }
    }
    private void FixedUpdate()
    {
        Raycasting();
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.x == 0 && transform.parent.GetComponent<Rigidbody2D>().velocity.y == 0) // not moving at all
        {
            moving = false;
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (transform.parent.GetComponent<Rigidbody2D>().velocity.x == 0 && transform.parent.GetComponent<Rigidbody2D>().velocity.y != 0) // moving vertically
        {
            moving = true;
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (transform.parent.GetComponent<Rigidbody2D>().velocity.x != 0 && transform.parent.GetComponent<Rigidbody2D>().velocity.y == 0) // horizontally
        {
            moving = true;
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        //Debug.Log(moving);

        if (moving == false)
        {
            if (gameObject.GetComponentInParent<AudioSource>().enabled == false) // if audio source is off, toggle it on
            {
                // this triggers when it collides with a wall
                gameObject.GetComponentInParent<Animator>().SetBool("Moving", false);
                gameObject.GetComponentInParent<Animator>().SetTrigger("Smack");

                gameObject.GetComponentInParent<AudioSource>().enabled = true;
                ParticlesClone = Instantiate(DustParticles, transform.parent.position, Quaternion.identity);
                Destroy(ParticlesClone, 3f);
            }

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponentInParent<AudioSource>().enabled = false;
        }

    }
    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(transform.parent.position, Player.position, Color.green);
        SightBlocked = Physics2D.Linecast(transform.parent.position, Player.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
