using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    //public static bool OnPlatform; // for the player

    public bool HitGround;
    public Transform Endline;
    public bool falling;
    public CapsuleCollider2D Standzone;
    public BoxCollider2D JumpZone;
    float fallVelocity;
    public float fallSpeedCap;
    public float fallDelay;
    public bool steppedOn;
    float SpinSpeed = 1f;



    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        fallVelocity = GetComponent<Rigidbody2D>().velocity.y;
        GetComponent<Animator>().SetFloat("SpinSpeed", SpinSpeed);


        if (steppedOn)
        {
            SpinSpeed -= Time.smoothDeltaTime;
            if (SpinSpeed <= 0)
            {
                SpinSpeed = 0;
            }

            if (fallDelay > 0)
            {
                fallDelay -= Time.smoothDeltaTime;
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Fall");
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                falling = true;
            }
        }
        if (HitGround)
        {
            falling = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Standzone.enabled = false;
            JumpZone.enabled = false;
        }
        else if (falling)
        {
            RayCastGround();
            if (fallVelocity < -fallSpeedCap) //fall speed cap
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * GetComponent<Rigidbody2D>().gravityScale * 100);
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            //if (fallDelay > 0)
            //{
            //    fallDelay -= Time.smoothDeltaTime;
            //}
            //else
            //{ 
            //GetComponent<Animator>().SetTrigger("Fall");
            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //falling = true;
            //    }
            steppedOn = true;
        }
    }


    public void RayCastGround()
    {
        Debug.DrawLine(transform.position, Endline.position, Color.green);
        HitGround = Physics2D.Linecast(transform.position, Endline.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
