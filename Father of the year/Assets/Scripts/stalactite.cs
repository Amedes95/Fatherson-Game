using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite : MonoBehaviour
{
    public bool walkedUnder;
    public float fallDelay;
    bool falling;
    bool hitGround;
    public PolygonCollider2D killZone;
    public Transform endLine;
    float fallVelocity;
    public float fallSpeedCap;

    public float GlintFrequency;
    float GlintCopy;

    public ParticleSystem FallingDirt;
    AudioSource StalactiteAudio;

    void Awake()
    {
        StalactiteAudio = gameObject.GetComponent<AudioSource>();
        fallDelay += Random.Range(0f, 2f);
        GlintCopy = GlintFrequency;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void FixedUpdate()
    {
        fallVelocity = GetComponent<Rigidbody2D>().velocity.y;

        if (walkedUnder)
        {
            if (fallDelay >= 0)
            {
                fallDelay -= Time.smoothDeltaTime;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                falling = true;
            }

            if (hitGround)
            {
                falling = false;
                killZone.enabled = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                walkedUnder = false;
            }
            else if (falling)
            {
                RaycastGround();
                if (fallVelocity < -fallSpeedCap) //fall speed cap
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * GetComponent<Rigidbody2D>().gravityScale * 100);
                }
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        GlintFrequency -= Time.smoothDeltaTime; // makes it glint every now and then
        if (GlintFrequency <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Glint");
            GlintFrequency = GlintCopy;
        }
    }

    public void RaycastGround()
    {
        Debug.DrawLine(transform.position, endLine.position, Color.green);
        hitGround = Physics2D.Linecast(transform.position, endLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            walkedUnder = true;
            FallingDirt.Play();
            StalactiteAudio.Play();
        }
    }
}
