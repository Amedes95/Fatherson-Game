using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayFloor : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    float WaitTime;
    string DropInput;
    string JumpInput;

    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // first see if we are using a controller or not
        if (Boombox.ControllerModeEnabled)
        {
            DropInput = "DropController";
            JumpInput = "JumpController";
            if (Input.GetAxis(DropInput) != -1) // not holding down
            {
                WaitTime = .5f;
            }
            if (Input.GetAxis(DropInput) == -1) // holding down
            {
                if (WaitTime <= 0)
                {
                    effector2D.rotationalOffset = 180;
                    WaitTime = .5f;
                }
                else
                {
                    WaitTime -= Time.deltaTime;
                }
            }
            if (Input.GetButton(JumpInput)) // jump is pressed
            {
                effector2D.rotationalOffset = 0;
            }
        }
        else
        {
            DropInput = "Drop";
            JumpInput = "Jump";
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
    }
}
