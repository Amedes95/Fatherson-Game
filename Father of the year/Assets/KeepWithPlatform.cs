using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWithPlatform : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponent<Collider2D>().transform.parent.SetParent(transform);
            if (PlayerMovement.moveHorizontal == 0)
            {
                collision.GetComponentInParent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponent<Collider2D>().transform.parent.SetParent(null);
        }
    }
}
