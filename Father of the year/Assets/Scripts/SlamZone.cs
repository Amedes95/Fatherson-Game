using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponentInParent<Animator>().SetBool("SlamRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponentInParent<Animator>().SetBool("SlamRange", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        }
    }
}
