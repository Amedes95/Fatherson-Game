using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyWeb : MonoBehaviour
{
    public static bool StuckInWeb;


    private void OnTriggerStay2D(Collider2D collision) // enter the web
    {
        if (collision.tag == "Player")
        {
            StuckInWeb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // leave the web
    {
        if (collision.tag == "Player")
        {
            StuckInWeb = false;
        }
    }
}
