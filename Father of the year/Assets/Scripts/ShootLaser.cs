using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{

    FleaController FleaBoss;

    private void Awake()
    {
        FleaBoss = gameObject.GetComponentInParent<FleaController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponentInParent<Animator>().SetBool("Walking", false);
            gameObject.GetComponentInParent<Animator>().SetBool("Shooting", true);
            FleaBoss.Walking = false;
            FleaBoss.Shooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FleaBoss.Walking = true;
            FleaBoss.Shooting = false;
            gameObject.GetComponentInParent<Animator>().SetBool("Walking", true);
            gameObject.GetComponentInParent<Animator>().SetBool("Shooting", false);

        }
    }
}
