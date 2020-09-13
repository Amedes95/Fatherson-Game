using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{
    public bool Ghost;
    public static bool Frozen;
    public float FreezeTimer;
    float FreezeTimerCopy;

    // Start is called before the first frame update
    void Start()
    {
        Frozen = false;
        FreezeTimerCopy = FreezeTimer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Ghost)
            {
                gameObject.GetComponentInParent<Animator>().SetTrigger("Touched");
            }
            Frozen = true;
            FreezeTimer = FreezeTimerCopy;
        }
    }

    private void FixedUpdate()
    {
        if (Frozen)
        {
            FreezeTimer -= Time.smoothDeltaTime; // count down
        }
        else
        {
            FreezeTimer = FreezeTimerCopy;
        }
        if (FreezeTimer <= 0)
        {
            Frozen = false;
            FreezeTimer = FreezeTimerCopy; // no negatives
        }
    }

}
