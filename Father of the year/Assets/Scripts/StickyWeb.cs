using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWeb : MonoBehaviour
{
    public static bool StuckInWeb;
    public bool ExplodeOnTouch;
    public GameObject ExplodeParticles;
    public static GameObject ExplodeClone;
    float StuckTimer;
    public bool Particles;

    private void Awake()
    {
        StuckTimer = .25f;
        StuckInWeb = false;
    }

    private void OnTriggerStay2D(Collider2D collision) // enter the web
    {
        if (collision.gameObject.activeInHierarchy)
        {
            if (collision.tag == "Player")
            {
                StuckInWeb = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // leave the web
    {
        if (collision.tag == "Player")
        {
            StuckInWeb = false;
        }
    }

    private void OnDisable()
    {
        StuckInWeb = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.activeInHierarchy)
        {
            if (collision.tag == "Player")
            {
                if (ExplodeOnTouch)
                {
                    gameObject.SetActive(false);
                    ExplodeClone = Instantiate(ExplodeParticles, transform.position, Quaternion.identity);
                    Destroy(ExplodeClone, 4f);
                }
                if (Particles)
                {
                    StuckTimer = .25f;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Particles)
        {
            if (StuckTimer > 0)
            {
                StuckInWeb = true;
                StuckTimer -= Time.smoothDeltaTime;
            }
            else
            {
                StuckTimer = 0;
                StuckInWeb = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }

    }
}
