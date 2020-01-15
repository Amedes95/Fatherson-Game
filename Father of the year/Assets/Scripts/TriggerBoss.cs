using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponentInParent<FleaController>().Walking = true;
            gameObject.GetComponentInParent<FleaController>().FightTriggered = true;

            gameObject.SetActive(false);
        }

    }
}
