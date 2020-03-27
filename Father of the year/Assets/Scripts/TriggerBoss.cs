using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public bool Flea;
    public bool Mushroom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Flea)
            {
                gameObject.GetComponentInParent<FleaController>().Walking = true;
                gameObject.GetComponentInParent<FleaController>().FightTriggered = true;

                gameObject.SetActive(false);
            }
            if (Mushroom)
            {
                gameObject.GetComponentInParent<MushroomBoos>().Walking = true;
                gameObject.GetComponentInParent<MushroomBoos>().FightTriggered = true;

                gameObject.SetActive(false);
            }

        }

    }
}
