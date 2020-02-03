using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayFloor : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    float WaitTime;
    string DropInput;
    string JumpInput;
    bool insideZone;

    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponentInParent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {


        // first see if we are using a controller or not
        if (Boombox.ControllerModeEnabled)
        {
            DropInput = "DropController";
            JumpInput = "JumpController";
            if (insideZone)
            {

                if (Input.GetAxis(DropInput) != 1)
                {
                    WaitTime = .1f;
                }
                if (Input.GetAxis(DropInput) == 1) // holding down
                {
                    if (WaitTime <= 0)
                    {
                        effector2D.rotationalOffset = 180;
                        WaitTime = .1f;
                    }
                    else
                    {
                        WaitTime -= Time.deltaTime;
                    }
                }
                if (Input.GetButton(JumpInput))
                {
                    effector2D.rotationalOffset = 0;
                }
            }
            else
            {
                effector2D.rotationalOffset = 0;
            }
            if (Input.GetAxis(DropInput) != 1) // use buttons instead of joystick axis
            {
                WaitTime = .1f;
            }
            if (Input.GetButton(JumpInput))
            {
                effector2D.rotationalOffset = 0;
            }

        }
        else
        {
            DropInput = "Drop";
            JumpInput = "Jump";
            if (insideZone)
            {

                if (Input.GetButtonUp(DropInput)) // use buttons instead of joystick axis
                {
                    WaitTime = .1f;
                }
                if (Input.GetButton(DropInput))
                {
                    if (WaitTime <= 0)
                    {
                        effector2D.rotationalOffset = 180;
                        WaitTime = .1f;
                    }
                    else
                    {
                        WaitTime -= Time.deltaTime;
                    }
                }
                if (Input.GetButton(JumpInput))
                {
                    effector2D.rotationalOffset = 0;
                }
            }
            else
            {
                effector2D.rotationalOffset = 0;
            }
            if (Input.GetButtonUp(DropInput)) // use buttons instead of joystick axis
            {
                WaitTime = .1f;
            }
            if (Input.GetButton(JumpInput))
            {
                effector2D.rotationalOffset = 0;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            insideZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            insideZone = false;
        }
    }
}
