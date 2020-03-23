﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWithPlatform : MonoBehaviour
{
    public static bool OnPlatform; // for the player


    private void Awake()
    {
        OnPlatform = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            OnPlatform = true;
            collision.GetComponent<Collider2D>().transform.parent.SetParent(transform);
            if (PlayerMovement.moveHorizontal == 0)
            {
                collision.GetComponentInParent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            }
        }

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Collider2D>().transform.SetParent(transform);
            collision.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet" && collision.gameObject.activeInHierarchy)
        {
            OnPlatform = false;
            collision.GetComponent<Collider2D>().transform.parent.SetParent(null);
        }

        if (collision.tag == "Enemy" && collision.gameObject.activeInHierarchy)
        {
            collision.GetComponent<Collider2D>().transform.SetParent(null);
        }
    }
}
