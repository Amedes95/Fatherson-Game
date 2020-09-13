using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetector : MonoBehaviour
{
    public bool WithinRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            WithinRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            WithinRange = false;
            gameObject.GetComponentInChildren<Turret>().InSights = false;

        }
    }
}
