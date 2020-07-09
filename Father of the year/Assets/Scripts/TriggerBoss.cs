using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public bool Flea;
    public bool Mushroom;
    public bool IceSkeleton;
    public bool Cyclops;
    public bool Bunny;
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
            if (IceSkeleton)
            {
                gameObject.GetComponentInParent<Animator>().SetTrigger("Thaw");
                gameObject.SetActive(false);
            }
            if (Cyclops)
            {
                gameObject.GetComponentInParent<Animator>().SetBool("Walk", true);
                gameObject.GetComponentInParent<Cyclops>().Walking = true;
                gameObject.SetActive(false);
            }
            if (Bunny)
            {
                gameObject.GetComponentInParent<BunnyBoss>().Wakeup();
                gameObject.SetActive(false);
            }

        }

    }
}
