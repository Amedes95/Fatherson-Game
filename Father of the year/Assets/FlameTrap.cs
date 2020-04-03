using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    bool SteppedOn;
    float BurnDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SteppedOn) // whenever the player steps on the plate, set the timer
        {
            BurnDuration = 5f;
        }




        if (BurnDuration > 0 && !SteppedOn)
        {
            BurnDuration -= Time.smoothDeltaTime; // count down burn timer once stepped on
        }
        else if (BurnDuration <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Triggered", false); // stop burning animation
            BurnDuration = 0f; // no negatives pls :)
        }
    }

    private void OnTriggerStay2D(Collider2D collision) // when the player enters the trigger, start the burn timer and animation
    {
        if (collision.tag == "Player")
        {
            SteppedOn = true;
            gameObject.GetComponent<Animator>().SetBool("Triggered", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // when they step off, start counting down the timer
    {
        if (collision.tag == "Player")
        {
            SteppedOn = false;
        }
    }
}
