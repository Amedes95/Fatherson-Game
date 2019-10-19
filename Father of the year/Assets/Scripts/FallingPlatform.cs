using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public static bool OnPlatform; // for the player

    public bool HitGround;
    public Transform Endline;
    public bool falling;
    public BoxCollider2D Standzone;
    public BoxCollider2D JumpZone;
    float fallVelocity;
    public float fallSpeedCap;


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        fallVelocity = GetComponent<Rigidbody2D>().velocity.y;
        if (falling)
        {
            RayCastGround();
            if (fallVelocity < -fallSpeedCap) //fall speed cap
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * GetComponent<Rigidbody2D>().gravityScale * 10);
            }
        }
        if (HitGround)
        {
            falling = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Standzone.enabled = false;
            JumpZone.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            GetComponent<Animator>().SetTrigger("Fall");
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            falling = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            OnPlatform = true;
            collision.GetComponent<Collider2D>().transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            OnPlatform = false;
            collision.GetComponent<Collider2D>().transform.SetParent(null);
        }
    }


    public void RayCastGround()
    {
        Debug.DrawLine(transform.position, Endline.position, Color.green);
        HitGround = Physics2D.Linecast(transform.position, Endline.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
