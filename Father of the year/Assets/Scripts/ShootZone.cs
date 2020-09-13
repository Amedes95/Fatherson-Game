using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootZone : MonoBehaviour
{

    IceSkeleton Boss;

    private void Awake()
    {
        Boss = gameObject.GetComponentInParent<IceSkeleton>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Boss.PhaseCounter == 2)
        {
            gameObject.GetComponentInParent<Animator>().SetBool("ShootRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && transform.parent.gameObject.activeInHierarchy)
        {
            gameObject.GetComponentInParent<Animator>().SetBool("ShootRange", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Boss.PhaseCounter == 2)
        {
            gameObject.GetComponentInParent<IceSkeleton>().FireDelay = .001f;
        }
    }
}
