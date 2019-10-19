using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponent<Collider2D>().transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponent<Collider2D>().transform.SetParent(null);
        }
    }

}
