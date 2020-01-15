using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHopTrigger : MonoBehaviour
{
    public float Force;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            collision.GetComponent<FleaController>().Hop(Force);
            collision.GetComponent<FleaController>().Walking = false;
        }
    }



}
