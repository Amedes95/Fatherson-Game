using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHelp : MonoBehaviour
{

    public Animator InfoBubble;
    public Animator ControllerBubble;

    bool ControllerActive;

    public GameObject Player;

    bool TouchingZone;

    public GameObject XboxJumpButton;
    public GameObject PS4JumpButton;
    public bool ControllerSpecial;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Boombox.ControllerModeEnabled)
            {
                ControllerBubble.SetBool("Helping", true);
            }
            else if (Boombox.ControllerModeEnabled == false)
            {
                InfoBubble.SetBool("Helping", true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Boombox.ControllerModeEnabled)
            {
                ControllerBubble.SetBool("Helping", false);
            }
            else
            {
                InfoBubble.SetBool("Helping", false);
            }
        }
        TouchingZone = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Boombox.ControllerModeEnabled)
        {
            InfoBubble.SetBool("Helping", false);
            ControllerBubble.SetBool("Helping", true);

        }
        else
        {
            ControllerBubble.SetBool("Helping", false);
            InfoBubble.SetBool("Helping", true);

        }
        TouchingZone = true;

    }

    private void Update()
    {
        if (Player.activeInHierarchy)
        {
            if (TouchingZone)
            {
                if (Boombox.ControllerModeEnabled)
                {
                    InfoBubble.SetBool("ControllerActive", true);
                    ControllerBubble.SetBool("ControllerActive", true);
                    ControllerBubble.SetBool("Helping", true);
                    if (ControllerSpecial)
                    {
                        if (Boombox.PS4Enabled)
                        {
                            XboxJumpButton.SetActive(false);
                            PS4JumpButton.SetActive(true);
                        }
                        else
                        {
                            XboxJumpButton.SetActive(true);
                            PS4JumpButton.SetActive(false);
                        }
                    }
                }
                else
                {
                    InfoBubble.SetBool("ControllerActive", false);
                    ControllerBubble.SetBool("ControllerActive", false);
                    InfoBubble.SetBool("Helping", true);

                }
            }

        }

    }
}
