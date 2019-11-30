using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().KillPlayer();
        }
        else if (collision.tag == "Feet")
        {
            if (collision.gameObject.activeInHierarchy)
            {
                collision.GetComponentInParent<PlayerHealth>().KillPlayer();
            }
        }
    }
}
